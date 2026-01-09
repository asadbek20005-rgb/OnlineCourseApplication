using OnlineCourse.Common.Extensions;
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

public class InstructorCourseService(IUnitOfWork unitOfWork, IContentService contentService, IUserHelper userHelper) : StatusGenericHandler, IInstructorCourseService
{
    private Guid UserId => Guid.Parse(userHelper.GetUserId());
    private readonly int[] Categories = [Commercial, Shop, Educate, Academy, Office];
    private readonly int[] Levels = [Beginner, Intermidiate, Expert];
    public async Task<string?> CreateCourseAsync(CreateCourseModel model)
    {
        await using var transaction = unitOfWork.BeginTransaction();
        try
        {
            if (!Categories.Where(x => x == model.CategoryId).Any())
            {
                AddError("Iltimos, to‘g‘ri kategoriyani tanlang.");
                return null;
            }

            if (!Levels.Where(x => x == model.LevelId).Any())
            {
                AddError("Iltimos, mavjud darajani tanlang.");
                return null;
            }

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
}
