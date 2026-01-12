using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Common.Extensions;
using OnlineCourse.Common.FilterOptions;
using OnlineCourse.Common.Models;
using OnlineCourse.Service.Instructor;

namespace OnlineCourse.Api.Controllers.Instructor;


public class LessonController(ILessonService service) : BaseInstructorController
{

    [HttpPost("{courseId}")]
    public async Task<IActionResult> GetAll(int courseId,LessonFilterOptions options)
    {
        var result = await service.GetAllAsync(courseId,options);
        if(service.IsValid) return Ok(result);
        return Ok(service.ToErrorResponse());
    }


    [HttpGet("{courseId}/{lessonId}")]
    public async Task<IActionResult> GetById(int courseId, int lessonId)
    {
        var result = await service.GetByIdAsync(courseId, lessonId);
        if (service.IsValid) return Ok(result);
        return Ok(service.ToErrorResponse());
    }

    [HttpPost]
    public async Task<IActionResult> CreateLessons([FromForm]CreateLessonsModel model)
    {
        var result = await service.CreateLessonsAsync(model.CourseId, model.Models);
        if (service.IsValid) return Ok(result);
        return BadRequest(service.ToErrorResponse());

    }

    [HttpPut("{courseId}/{lessonId}")]
    public async Task<IActionResult> Update(int courseId, int lessonId, UpdateLessonModel model)
    {
        var result = await service.UpdateLessonAsync(courseId, lessonId, model);
        if (service.IsValid) return Ok(result);
        return BadRequest(service.ToErrorResponse());
    }

    [HttpDelete("{courseId}/{lessonId}")]
    public async Task<IActionResult> Delete(int courseId, int lessonId)
    {
        var result = await service.DeleteLessonAsync(courseId, lessonId);
        if (service.IsValid) return Ok(result);
        return BadRequest(service.ToErrorResponse());
    }
}


public class CreateLessonsModel
{
    public int CourseId { get; set; }
    public List<CreateLessonModel> Models { get; set; }

}