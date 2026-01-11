using Microsoft.EntityFrameworkCore;
using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.Extensions;
using OnlineCourse.Common.FilterOptions;
using OnlineCourse.Common.Models;
using OnlineCourse.Data.Entites;
using OnlineCourse.Data.Repositories;
using OnlineCourse.Service.Helpers;
using StatusGeneric;

namespace OnlineCourse.Service.Instructor;
using static OnlineCourse.Common.Constants.MinioFolderConstant;
using static OnlineCourse.Common.Constants.StatusConstants;
public class LessonService(IUnitOfWork unitOfWork, IContentService contentService, IUserHelper userHelper) : StatusGenericHandler, ILessonService
{
    private readonly Guid UserId = Guid.Parse(userHelper.GetUserId());
    public async Task<string?> CreateLessonsAsync(int courseId, List<CreateLessonModel> models)
    {
        await using var transaction = unitOfWork.BeginTransaction();
        try
        {
            var id = await GetCourseIdAsync(courseId);
            if (id is null) return null;

            if(models.Count == 0 || models is null)
            {
                AddError("Ma?lumotlar topilmadi.");
                return null;
            }

            var lessonEntities = new List<Lesson>();
            foreach (var model in models)
            {
                if ((model.VideoContent is null || model.VideoContent.Length == 0) && string.IsNullOrEmpty(model.Title))
                {
                    AddError("");
                    return null;
                }

                var newLesson = model.MapToEntity<Lesson, CreateLessonModel>();
                newLesson.StatusId = Active;
                newLesson.VideoContentId = await contentService.CreateContentForVideo(model.VideoContent, LessonVideos) ?? 0;
                CombineStatuses(contentService);
                if (contentService.HasErrors) return null;
                newLesson.CreatedAt = DateTime.UtcNow;
                newLesson.CreatedUserId = UserId;
                lessonEntities.Add(newLesson);
            }
            await unitOfWork.LessonRepository().AddRangeAsync(lessonEntities.AsQueryable());
            await unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();
            return "Darsliklar muvaffaqiyatli qo'shildi!";
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            AddError(ex.ToString());
            throw;
        }
    }

    public Task<string?> DeleteLesson(int courseId, int lessonId)
    {
        throw new NotImplementedException();
    }

    public Task<PaginationModel<LessonDto>> GetAllAsync(LessonFilterOptions options)
    {
        throw new NotImplementedException();
    }

    public Task<PaginationModel<LessonDto>> GetAllAsync(int courseId, LessonFilterOptions options)
    {
        throw new NotImplementedException();
    }

    public Task<LessonDto?> GetByIdAsync(int courseId, int lessonId)
    {
        throw new NotImplementedException();
    }

    public Task<string?> UpdateLesson(int courseId, UpdateLessonModel model)
    {
        throw new NotImplementedException();
    }


    private async Task<int?> GetCourseIdAsync(int courseId)
    {
        var userCourseId = await unitOfWork.UserCourseRepository()
        .GetAll()
        .Where(x => x.UserId == UserId && x.IsActive && x.CourseId == courseId)
        .Select(x => x.CourseId)
        .FirstOrDefaultAsync();

        var id = await unitOfWork.CourseRepository()
            .GetAll()
            .Where(x => x.StatusId != Deleted && x.Id == userCourseId)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();


        if (id is 0)
        {
            AddError("");
            return null;
        }

        return id;
    }
}
