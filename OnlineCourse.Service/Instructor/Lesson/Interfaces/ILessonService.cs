using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.FilterOptions;
using OnlineCourse.Common.Models;
using StatusGeneric;

namespace OnlineCourse.Service.Instructor;

public interface ILessonService : IStatusGeneric
{
    Task<PaginationModel<LessonDto>> GetAllAsync(int courseId, LessonFilterOptions options);
    Task<LessonDto?> GetByIdAsync(int courseId, int lessonId);
    Task<string?> CreateLessonsAsync(int courseId, List<CreateLessonModel> models);
    Task<string?> UpdateLesson(int courseId, UpdateLessonModel model);
    Task<string?> DeleteLesson(int courseId, int lessonId);

}