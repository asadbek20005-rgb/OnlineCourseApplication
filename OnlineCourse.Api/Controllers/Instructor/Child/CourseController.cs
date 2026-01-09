using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Common.Extensions;
using OnlineCourse.Common.Models;
using OnlineCourse.Service.Instructor.Course.Interfaces;

namespace OnlineCourse.Api.Controllers.Instructor;

public class CourseController(IInstructorCourseService service) : BaseInstructorController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateCourseModel model)
    {
        var result = await service.CreateCourseAsync(model);
        if(service.IsValid) return Ok(result);
        return BadRequest(service.ToErrorResponse());
    }
}
