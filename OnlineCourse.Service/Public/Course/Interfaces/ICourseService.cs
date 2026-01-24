using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.FilterOptions;
using OnlineCourse.Common.Models;
using StatusGeneric;

namespace OnlineCourse.Service.Public;

public interface ICourseService : IStatusGeneric
{
    PaginationModel<CourseDto> GetAll(CourseFilterOptions options);
    Task<CourseDto?> GetByIdAsync(int courseID);
}

