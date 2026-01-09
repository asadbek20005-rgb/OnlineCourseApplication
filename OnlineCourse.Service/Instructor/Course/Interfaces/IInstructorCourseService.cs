using OnlineCourse.Common.Models;
using StatusGeneric;

namespace OnlineCourse.Service.Instructor.Course.Interfaces;

public interface IInstructorCourseService : IStatusGeneric
{
    Task<string?> CreateCourseAsync(CreateCourseModel model);
}