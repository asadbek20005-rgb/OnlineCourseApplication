using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.FilterOptions;
using OnlineCourse.Common.Models;
using StatusGeneric;

namespace OnlineCourse.Service.Student.Lesson;

public interface IStudentLessonService : IStatusGeneric
{
    Task<PaginationModel<LessonDto>?> GetAllAsync(int courseId, LessonFilterOptions options);
    Task<LessonDto?> GetByIdAsync(int courseId, int lessonId);
    Task<(Stream, string)> WatchVideo(int courseId, int lessonId, string url);
}