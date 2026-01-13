using Microsoft.AspNetCore.Http;
using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.FilterOptions;
using OnlineCourse.Common.Models;
using StatusGeneric;

namespace OnlineCourse.Service.Instructor.Course.Interfaces;

public interface IInstructorCourseService : IStatusGeneric
{
    Task<PaginationModel<CourseDto>> GetAllAsync(CourseFilterOptions options);
    Task<CourseDto?> GetCourseByIdAsync(int courseId);
    Task<string?> CreateCourseAsync(CreateCourseModel model);
    Task<string?> UpdateCourseAsync(int courseId,UpdateCourseModel model);
    Task<string?> DeleteCourseAsync(int courseId);
    Task<string?> UpdateCoursePhotoAsync(int courseId, IFormFile file);
    Task<string?> MakeActiveAsync(int courseId);
    Task<string?> MakeDeActiveAsync(int courseId);
}