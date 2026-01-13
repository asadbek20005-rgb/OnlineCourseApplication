using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.Extensions;
using OnlineCourse.Common.FilterOptions;
using OnlineCourse.Common.Models;
using OnlineCourse.Data.Repositories;
using OnlineCourse.Service.Helpers;
using OnlineCourse.Service.Instructor;
using OnlineCourse.Service.Student.Lesson;
using StatusGeneric;

namespace OnlineCourse.Service.Student;
using static OnlineCourse.Common.Constants.MinioFolderConstant;
using static OnlineCourse.Common.Constants.StatusConstants;
public class StudentLessonService(IUnitOfWork unitOfWork, IContentService contentService, IUserHelper userHelper) : StatusGenericHandler, IStudentLessonService
{
    private Guid UserId => Guid.Parse(userHelper.GetUserId());

    public async Task<PaginationModel<LessonDto>?> GetAllAsync(int courseId, LessonFilterOptions options)
    {
        var id = await GetCourseIdAsync(courseId);
        if (id is 0) return null;

        var lessons = unitOfWork.LessonRepository()
            .GetAll(x => x.Status)
            .Where(x => x.CourseId == id && x.StatusId != Deleted);

        var (resultQuery, totalCount) = lessons.ApplyFilter(options);

        var config = GetCustomConfig();
        var dtos = resultQuery.MapToDtos<Data.Entites.Lesson, LessonDto>(config)
            .ToPaginationModel(options.Page, options.PageSize, totalCount);


        var co = await lessons.ToListAsync();

        return dtos;

    }

    public async Task<LessonDto?> GetByIdAsync(int courseId, int lessonId)
    {
        var id = await GetCourseIdAsync(courseId);
        if (id is 0) return null;

        var lesson = await GetLessonByIdAsync(courseId, lessonId);
        if (lesson is null) return null;

        var config = GetCustomConfig();

        return lesson.MapToDto<Data.Entites.Lesson, LessonDto>(config);

    }

    public async Task<(Stream, string)> WatchVideo(int courseId, int lessonId, string url)
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

    private async Task<int> GetCourseIdAsync(int courseId)
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
            AddError("Kurs topilmadi");
            return 0;
        }

        return id;
    }

    private TypeAdapterConfig GetCustomConfig()
    {
        var config = new TypeAdapterConfig();

        config.NewConfig<Data.Entites.Lesson, LessonDto>()
            .Map(dest => dest.StatusName, src => src.Status.ShortName)
            .Map(dest => dest.VideoContentUrl, src => src.VideoContent.Url);

        return config;
    }

    private async Task<Data.Entites.Lesson?> GetLessonByIdAsync(int courseId, int id)
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
