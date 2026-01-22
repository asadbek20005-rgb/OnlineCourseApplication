using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Common.Extensions;
using OnlineCourse.Common.FilterOptions;
using OnlineCourse.Service.Public;

namespace OnlineCourse.Api.Controllers.Public;

public class CourseController(ICourseService courseService) : BasePublicController
{
    [HttpPost]
    public IActionResult GetAll(CourseFilterOptions options)
    {
        var result = courseService.GetAll(options);
        if (courseService.IsValid) return Ok(result);

        return BadRequest(courseService.ToErrorResponse());
    }
}