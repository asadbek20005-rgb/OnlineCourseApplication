using Mapster;
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

            if (models.Count == 0 || models is null)
            {
                AddError("Ma?lumotlar topilmadi.");
                return null;
            }

            var lessonEntities = new List<Lesson>();
            foreach (var model in models)
            {
                if ((model.File is null || model.File.Length == 0) && string.IsNullOrEmpty(model.Title))
                {
                    AddError("");
                    return null;
                }

                var newLesson = model.MapToEntity<Lesson, CreateLessonModel>();
                newLesson.StatusId = Active;
                newLesson.VideoContentId = await contentService.CreateContentForVideo(model.File, LessonVideos) ?? 0;
                CombineStatuses(contentService);
                if (contentService.HasErrors) return null;
                newLesson.CreatedAt = DateTime.UtcNow;
                newLesson.CreatedUserId = UserId;
                newLesson.CourseId = courseId;
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

    public async Task<string?> DeleteLessonAsync(int courseId, int lessonId)
    {
        var course_id = await GetCourseIdAsync(courseId);
        if (course_id is null) return null;

        var lesson = await GetLessonByIdAsync(courseId, lessonId);
        if (lesson is null) return null;

        lesson.StatusId = Deleted;
        lesson.UpdatedAt = DateTime.UtcNow;
        lesson.UpdatedUserId = UserId;

        unitOfWork.LessonRepository().Update(lesson);
        await unitOfWork.SaveChangesAsync();

        return "Darslik muvaffaqiyatli o'chirildi!";
    }

    public async Task<PaginationModel<LessonDto>?> GetAllAsync(int courseId, LessonFilterOptions options)
    {
        var id = await GetCourseIdAsync(courseId);
        if (id is 0) return null;

        var lessons = unitOfWork.LessonRepository()
            .GetAll(x => x.Status)
            .Where(x => x.CourseId == id && x.StatusId != Deleted);

        var (resultQuery, totalCount) = lessons.ApplyFilter(options);

        var config = GetCustomConfig();
        var dtos = resultQuery.MapToDtos<Lesson, LessonDto>(config)
            .ToPaginationModel(options.Page, options.PageSize, totalCount);

        return dtos;
    }

    public async Task<LessonDto?> GetByIdAsync(int courseId, int lessonId)
    {
        var id = await GetCourseIdAsync(courseId);
        if (id is 0) return null;

        var lesson = await GetLessonByIdAsync(courseId, lessonId);
        if (lesson is null) return null;

        var config = GetCustomConfig();

        return lesson.MapToDto<Lesson, LessonDto>(config);

    }

    public async Task<string?> UpdateLessonAsync(int courseId, int lessonId, UpdateLessonModel model)
    {
        await using var transaction = unitOfWork.BeginTransaction();
        try
        {
            var course_id = await GetCourseIdAsync(courseId);
            if (course_id is null) return null;

            var lesson = await GetLessonByIdAsync(courseId,lessonId);
            if (lesson is null) return null;

            lesson = model.MapForUpdate(lesson);

            if (model.File is not null && model.File.Length != 0)
            {
                lesson.VideoContentId = await contentService.CreateContentForVideo(model.File, LessonVideos) ?? 0;
                CombineStatuses(contentService);
                if (contentService.HasErrors) return null;
            }
            lesson.StatusId = Updated;
            lesson.UpdatedAt = DateTime.UtcNow;
            lesson.UpdatedUserId = UserId;

            unitOfWork.LessonRepository().Update(lesson);
            await unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();
            return "Darslik muvaffaqiyatli yangilandi!";
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            AddError(ex.Message);
            throw;
        }



    }

    public async Task<(Stream, string)> WatchVideoAsync(int courseId, int lessonId)
    {
        var id = await GetCourseIdAsync(courseId);
        if (id is 0) return (Stream.Null, string.Empty);

        var lesson = await GetLessonByIdAsync(courseId, lessonId);
        if (lesson is null) return (Stream.Null, string.Empty);

        var (stream, contentType) = await contentService.GetContentForVideo(lesson.VideoContentId, LessonVideos, lesson.VideoContent.Url);

        CombineStatuses(contentService);
        if (contentService.HasErrors) return (Stream.Null, string.Empty);

        return (stream, contentType);
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
            AddError("Kurs Topilmadi!");
            return null;
        }

        return id;
    }

    private TypeAdapterConfig GetCustomConfig()
    {
        var config = new TypeAdapterConfig();

        config.NewConfig<Lesson, LessonDto>()
            .Map(dest => dest.StatusName, src => src.Status.ShortName);

        return config;
    }
    
    private async Task<Lesson?> GetLessonByIdAsync(int courseId,int id)
    {
        var lesson = await unitOfWork.LessonRepository()
            .GetAll(x => x.Status, x => x.VideoContent)
            .Where(x => x.CourseId == courseId && x.Id == id && x.StatusId != Deleted)
            .FirstOrDefaultAsync();

        if (lesson is null)
        {
            AddError("Darslik topilmadi!");
            return null;
        }


        return lesson;
    }
}