using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.Models;
using StatusGeneric;

namespace OnlineCourse.Service.Instructor.Course.Interfaces;

public interface IInstructorCourseService : IStatusGeneric
{
    Task<List<CourseDto>> GetAllAsync();
    Task<string?> CreateCourseAsync(CreateCourseModel model);
}