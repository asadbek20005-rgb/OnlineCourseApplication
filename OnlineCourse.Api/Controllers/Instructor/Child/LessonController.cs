using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Common.Extensions;
using OnlineCourse.Common.Models;
using OnlineCourse.Service.Instructor;

namespace OnlineCourse.Api.Controllers.Instructor;


public class LessonController(ILessonService service) : BaseInstructorController
{

    [HttpPost()]
    public async Task<IActionResult> CreateLessons([FromForm]CreateLessonsModel model)
    {
        var result = await service.CreateLessonsAsync(model.CourseId, model.Models);
        if (service.IsValid) return Ok(result);
        return BadRequest(service.ToErrorResponse());

    }

}


public class CreateLessonsModel
{
    public int CourseId { get; set; }
    public List<CreateLessonModel> Models { get; set; }

}