using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.FilterOptions;
using OnlineCourse.Common.Models;
using StatusGeneric;

namespace OnlineCourse.Service.Instructor;

public interface ILessonService : IStatusGeneric
{
    Task<PaginationModel<LessonDto>?> GetAllAsync(int courseId, LessonFilterOptions options);
    Task<LessonDto?> GetByIdAsync(int courseId, int lessonId);
    Task<string?> CreateLessonsAsync(int courseId, List<CreateLessonModel> models);
    Task<string?> UpdateLessonAsync(int courseId, int lessonId,UpdateLessonModel model);
    Task<string?> DeleteLessonAsync(int courseId, int lessonId);

}