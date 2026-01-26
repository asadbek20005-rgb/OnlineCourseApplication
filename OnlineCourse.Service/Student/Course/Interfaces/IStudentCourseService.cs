using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.FilterOptions;
using OnlineCourse.Common.Models;
using StatusGeneric;

namespace OnlineCourse.Service.Student.Course.Interfaces;

public interface IStudentCourseService : IStatusGeneric
{
    Task<PaginationModel<CourseDto>> GetAllAsync(CourseFilterOptions options);
    Task<PaginationModel<CourseDto>> GetUnEnrolledCourses(CourseFilterOptions options);
    Task<CourseDto?> GetByIdAsync(int courseId);
    Task<string?> EnrollAsync(int courseId);
    Task<string?> UnEnrollAsync(int courseId);
}