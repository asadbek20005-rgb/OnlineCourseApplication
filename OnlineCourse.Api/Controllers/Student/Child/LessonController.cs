using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Common.Extensions;
using OnlineCourse.Common.FilterOptions;
using OnlineCourse.Service.Student.Lesson;

namespace OnlineCourse.Api.Controllers.Student;

public class LessonController(IStudentLessonService service) : BaseStudentController
{
    [HttpPost("{courseId}")]
    public async Task<IActionResult> GetAll(int courseId, LessonFilterOptions options)
    {
        var result = await service.GetAllAsync(courseId, options);
        if (service.IsValid) return Ok(result);
        return BadRequest(service.ToErrorResponse());
    }


    [HttpGet("{courseId}/{lessonId}")]
    public async Task<IActionResult> GetById(int courseId, int lessonId)
    {
        var result = await service.GetByIdAsync(courseId, lessonId);
        if (service.IsValid) return Ok(result);
        return BadRequest(service.ToErrorResponse());
    }

    [HttpGet("{courseId}/{lessonId}")]
    public async Task<IActionResult> WatchVideo(int courseId, int lessonId, string url)
    {
        var (stream, contentType) = await service.WatchVideo(courseId, lessonId, url);
        if (service.IsValid) return File(stream, contentType);
        return BadRequest(service.ToErrorResponse());
    }

}
