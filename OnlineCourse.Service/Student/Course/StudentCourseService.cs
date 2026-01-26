using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.Extensions;
using OnlineCourse.Common.FilterOptions;
using OnlineCourse.Common.Models;
using OnlineCourse.Data.Entites;
using OnlineCourse.Data.Repositories;
using OnlineCourse.Service.Helpers;
using OnlineCourse.Service.Instructor;
using OnlineCourse.Service.Student.Course.Interfaces;
using StatusGeneric;

namespace OnlineCourse.Service.Student;
using static OnlineCourse.Common.Constants.StatusConstants;
public class StudentCourseService(IUnitOfWork unitOfWork, IUserHelper userHelper) : StatusGenericHandler, IStudentCourseService
{
    private Guid UserId => Guid.Parse(userHelper.GetUserId());
    public async Task<string?> EnrollAsync(int courseId)
    {
        var id = await unitOfWork.CourseRepository()
            .GetAll()
            .Where(x => x.Id == courseId && x.StatusId != Deleted)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();

        if (id is 0)
        {
            AddError("Kurs topilmadi!");
            return null;
        }

        if (await unitOfWork.UserCourseRepository().GetAll().Where(x => x.UserId == UserId && x.CourseId == courseId).AnyAsync())
        {
            AddError("Siz allaqachon kursga yozilib bo'lgansiz! ");
            return null;
        }

        var userCourse = new UserCourse
        {
            UserId = UserId,
            CourseId = courseId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedUserId = UserId
        };

        await unitOfWork.UserCourseRepository().AddAsync(userCourse);
        await unitOfWork.SaveChangesAsync();

        return "Kursga muvaffaqiyatli a'zo bo'ldingiz!";
    }

    public async Task<PaginationModel<CourseDto>> GetAllAsync(CourseFilterOptions options)
    {
        var userCourseIds = await unitOfWork.UserCourseRepository()
            .GetAll()
            .Where(x => x.UserId == UserId && x.IsActive)
            .Select(x => x.CourseId)
            .ToListAsync();

        var query = unitOfWork.CourseRepository()
            .GetAll(x => x.Status, x => x.Category, x => x.Level)
            .Where(x => userCourseIds.Contains(x.Id) && x.StatusId != Deleted);

        var (queryResult, totalCount) = query.ApplyFilter(options);

        var config = GetCustomConfig();
        var result = queryResult.MapToDtos<Data.Entites.Course, CourseDto>(config)
            .ToPaginationModel(options.Page, options.PageSize, totalCount);

        return result;


    }
    public async Task<PaginationModel<CourseDto>> GetUnEnrolledCourses(CourseFilterOptions options)
    {
        var courseIds = await unitOfWork.UserCourseRepository().GetAll().Where(x => x.UserId == UserId && x.IsActive)
            .Select(x => x.CourseId)
            .ToListAsync();

        var coursesQuery = unitOfWork.CourseRepository().GetAll(x => x.Status, x => x.Category, x => x.Level)
            .Where(x => !courseIds.Contains(x.Id) && x.StatusId == Public);
        var (queryResult, totalCount) = coursesQuery.ApplyFilter(options);

        var config = GetCustomConfig();

        var course = queryResult.MapToDtos<OnlineCourse.Data.Entites.Course, CourseDto>(config)
            .ToPaginationModel(options.Page, options.PageSize, totalCount);

        return course;
    }

    public async Task<CourseDto?> GetByIdAsync(int courseId)
    {
        var course = await GetCourseByIdAsync(courseId);
        if (course is null) return null;

        var config = GetCustomConfig();

        var courseDto = course.MapToDto<Data.Entites.Course, CourseDto>(config);
        return courseDto;
    }

    public async Task<string?> UnEnrollAsync(int courseId)
    {
        var id = await unitOfWork.CourseRepository()
          .GetAll()
          .Where(x => x.Id == courseId && x.StatusId != Deleted)
          .Select(x => x.Id)
          .FirstOrDefaultAsync();

        var userCourse = await unitOfWork.UserCourseRepository()
            .GetAll()
            .Where(x => x.IsActive && x.UserId == UserId && x.CourseId == id)
            .FirstOrDefaultAsync();


        if (userCourse is null)
        {
            AddError("Kurs topilmadi!");
            return null;
        }
        unitOfWork.UserCourseRepository().Delete(userCourse);
        await unitOfWork.SaveChangesAsync();
        return "Bekor qilindi!";
    }

    private TypeAdapterConfig GetCustomConfig()
    {
        var config = new TypeAdapterConfig();
        config.NewConfig<Data.Entites.Course, CourseDto>()
            .Map(x => x.StatusName, x => x.Status.FullName)
            .Map(x => x.LevelName, x => x.Level.FullName)
            .Map(x => x.CategoryName, x => x.Category.FullName)
            .Map(x => x.PhotoContentUrl, x => x.Content.Url);

        return config;

    }

    private async Task<Data.Entites.Course?> GetCourseByIdAsync(int courseId)
    {

        var userCourseId = await unitOfWork
                   .UserCourseRepository()
                   .GetAll()
                   .Where(x => x.UserId == UserId && x.IsActive && x.CourseId == courseId)
                   .Select(x => x.CourseId)
                   .FirstOrDefaultAsync();

        var course = await unitOfWork
            .CourseRepository()
            .GetAll(x => x.Status, x => x.Level, x => x.Category, x => x.Content)
            .Where(x => x.StatusId != Deleted && x.Id == userCourseId)
            .FirstOrDefaultAsync();

        if (course is null)
        {
            AddError("Kurs topilmadi. Iltimos, keyinroq qayta urinib ko‘ring.");
            return null;
        }


        return course;
    }


}