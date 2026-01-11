using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Common.Extensions;
using OnlineCourse.Common.FilterOptions;
using OnlineCourse.Common.Models;
using OnlineCourse.Service.Instructor.Course.Interfaces;

namespace OnlineCourse.Api.Controllers.Instructor;

public class CourseController(IInstructorCourseService service) : BaseInstructorController
{
    [HttpPost]
    public async Task<IActionResult> GetAll(CourseFilterOptions options)
    {
        var result = await service.GetAllAsync(options);

        if (service.IsValid) return Ok(result);
        return BadRequest(service.ToErrorResponse());
    }

    [HttpGet("{courseId}")]
    public async Task<IActionResult> GetById(int courseId)
    {
        var result = await service.GetCourseByIdAsync(courseId);
        if (service.IsValid) return Ok(result);
        return BadRequest(service.ToErrorResponse());
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateCourseModel model)
    {
        var result = await service.CreateCourseAsync(model);
        if (service.IsValid) return Ok(result);
        return BadRequest(service.ToErrorResponse());
    }

    [HttpPut("{courseId}")]
    public async Task<IActionResult> Update(int courseId, UpdateCourseModel model)
    {
        var result = await service.UpdateCourseAsync(courseId, model);
        if (service.IsValid) return Ok(result);
        return BadRequest(service.ToErrorResponse());
    }

    [HttpPut("{courseId}")]
    public async Task<IActionResult> UpdatePhoto(int courseId, IFormFile file)
    {
        var result = await service.UpdateCoursePhotoAsync(courseId, file);
        if (service.IsValid) return Ok(result);
        return BadRequest(service.ToErrorResponse());
    }

    [HttpDelete("{courseId}")]
    public async Task<IActionResult> Delete(int courseId)
    {
        var result = await service.DeleteCourseAsync(courseId);
        if (service.IsValid) return Ok(result);
        return BadRequest(service.ToErrorResponse());
    }

}
