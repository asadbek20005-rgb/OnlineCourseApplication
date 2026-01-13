using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.Extensions;
using OnlineCourse.Common.FilterOptions;
using OnlineCourse.Common.Models;
using OnlineCourse.Data.Entites;
using OnlineCourse.Data.Repositories;
using OnlineCourse.Service.Helpers;
using OnlineCourse.Service.Instructor.Course.Interfaces;
using StatusGeneric;

namespace OnlineCourse.Service.Instructor;
using static OnlineCourse.Common.Constants.CategoryConstant;
using static OnlineCourse.Common.Constants.LevelConstant;
using static OnlineCourse.Common.Constants.MinioFolderConstant;
using static OnlineCourse.Common.Constants.StatusConstants;

public class InstructorCourseService(IUnitOfWork unitOfWork,
    IContentService contentService,
    IUserHelper userHelper)
    : StatusGenericHandler, IInstructorCourseService
{
    private Guid UserId => Guid.Parse(userHelper.GetUserId());
    private readonly int[] Categories = [Commercial, Shop, Educate, Academy, Office];
    private readonly int[] Levels = [Beginner, Intermidiate, Expert];


    public async Task<string?> CreateCourseAsync(CreateCourseModel model)
    {
        await using var transaction = unitOfWork.BeginTransaction();
        try
        {
            if (!ValidateCategoryAndLevel(model.CategoryId, model.LevelId)) return null;

            var newCourse = model.MapToEntity<Data.Entites.Course, CreateCourseModel>();
            newCourse.StatusId = Active;
            newCourse.PhotoContentId = await contentService.CreateContentForImage(model.PhotoContent, CourseImages) ?? 0;
            CombineStatuses(contentService);
            if (contentService.HasErrors) return null;

            await unitOfWork.CourseRepository().AddAsync(newCourse);
            await unitOfWork.SaveChangesAsync();


            var newUserCourse = new UserCourse
            {
                UserId = UserId,
                CourseId = newCourse.Id,
                CreatedAt = DateTime.UtcNow,
                CreatedUserId = UserId,
                IsActive = true,
            };

            await unitOfWork.UserCourseRepository().AddAsync(newUserCourse);
            await unitOfWork.SaveChangesAsync();

            await transaction.CommitAsync();
            return "Kurs muvaffaqiyatli qo'shildi!";
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception(ex.Message);
        }


    }

    public async Task<PaginationModel<CourseDto>> GetAllAsync(CourseFilterOptions options)
    {
        var courseIds = await unitOfWork
            .UserCourseRepository()
            .GetAll()
            .Where(x => x.UserId == UserId && x.IsActive)
            .Select(x => x.CourseId)
            .ToListAsync();

        var courses = unitOfWork
            .CourseRepository()
            .GetAll(x => x.Status, x => x.Level, x => x.Category, x => x.Content)
            .Where(x => x.StatusId != Deleted && courseIds.Contains(x.Id));
        var config = GetCustomConfig();

        var (resultQuery, totalCount) = courses.ApplyFilter(options);

        var result = resultQuery.MapToDtos<Data.Entites.Course, CourseDto>(config)
            .ToPaginationModel(options.Page, options.PageSize, totalCount);

        return result;
    }

    public async Task<CourseDto?> GetCourseByIdAsync(int courseId)
    {
        var course = await GetByIdAsync(courseId);
        if (course is null) return null;

        var config = GetCustomConfig();

        var courseDto = course.MapToDto<Data.Entites.Course, CourseDto>(config);
        return courseDto;
    }

    public async Task<string?> UpdateCourseAsync(int courseId, UpdateCourseModel model)
    {
        if (model.CategoryId.HasValue)
        {
            if (!Categories.Where(x => x == model.CategoryId).Any())
            {
                AddError("Iltimos, to‘g‘ri kategoriyani tanlang.");
                return null;
            }
        }

        if (model.LevelId.HasValue)
        {
            if (!Levels.Where(x => x == model.LevelId).Any())
            {
                AddError("Iltimos, mavjud darajani tanlang.");
                return null;
            }
        }


        var course = await GetByIdAsync(courseId);
        if (course is null) return null;

        course = model.MapForUpdate(course);

        course.StatusId = Updated;
        course.UpdatedAt = DateTime.UtcNow;
        course.UpdatedUserId = UserId;

        unitOfWork.CourseRepository().Update(course);
        await unitOfWork.SaveChangesAsync();
        return "Kurs muvaffaqiyatli yangilandi!";
    }

    public async Task<string?> DeleteCourseAsync(int courseId)
    {
        var course = await GetByIdAsync(courseId);
        if (course is null) return null;

        course.StatusId = Deleted;

        unitOfWork.CourseRepository().Update(course);
        await unitOfWork.SaveChangesAsync();
        return "Kurs muvaffaqiyatli o'chirildi!";
    }

    public async Task<string?> UpdateCoursePhotoAsync(int courseId, IFormFile file)
    {
        await using var transaction = unitOfWork.BeginTransaction();
        try
        {
            var course = await GetByIdAsync(courseId);
            if (course is null) return null;

            course.PhotoContentId = await contentService.UpdateContentForImage(course.PhotoContentId, file) ?? 0;
            CombineStatuses(contentService);
            if (contentService.HasErrors) return null;

            course.StatusId = Updated;

            course.UpdatedAt = DateTime.UtcNow;
            course.UpdatedUserId = UserId;

            unitOfWork.CourseRepository().Update(course);
            await unitOfWork.SaveChangesAsync();

            await transaction.CommitAsync();
            return "Kursning rasmi muvaffaqiyatli yuklandi!";

        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            AddError(ex.Message);
            throw;
        }




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

    private async Task<Data.Entites.Course?> GetByIdAsync(int courseId)
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

    private bool ValidateCategoryAndLevel(int categoryId, int levelId)
    {

        if (!Categories.Where(x => x == categoryId).Any())
        {
            AddError("Iltimos, to‘g‘ri kategoriyani tanlang.");
            return false;
        }

        if (!Levels.Where(x => x == levelId).Any())
        {
            AddError("Iltimos, mavjud darajani tanlang.");
            return false;
        }

        return true;
    }

    public Task<string?> MakeActiveAsync(int courseId)
    {
        throw new NotImplementedException();
    }

    public Task<string?> MakeDeActiveAsync(int courseId)
    {
        throw new NotImplementedException();
    }
}
