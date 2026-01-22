using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.Models;

namespace OnlineCourse.Service.Public;

public interface IHomeService
{
    Task<IEnumerable<CourseDto>> GetFeateredCourses();
    Task<IEnumerable<CategoryDto>> GetTopCategories();

    // Get Feedbacks
    // Get Articles

}