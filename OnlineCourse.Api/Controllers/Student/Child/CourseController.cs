using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Common.Extensions;
using OnlineCourse.Common.FilterOptions;
using OnlineCourse.Service.Student.Course.Interfaces;

namespace OnlineCourse.Api.Controllers.Student;

public class CourseController(IStudentCourseService studentCourseService) : BaseStudentController
{
    [HttpPost]
    public async Task<IActionResult> GetAll(CourseFilterOptions options)
    {
        var result = await studentCourseService.GetAllAsync(options);

        if (studentCourseService.IsValid) return Ok(result);

        return BadRequest(studentCourseService.ToErrorResponse());
    }


    [HttpGet("{courseId}")]
    public async Task<IActionResult> GetById(int courseId)
    {
        var result = await studentCourseService.GetByIdAsync(courseId);

        if (studentCourseService.IsValid) return Ok(result);

        return BadRequest(studentCourseService.ToErrorResponse());
    }


    [HttpPost]
    public async Task<IActionResult> Enroll(int courseId)
    {
        var result = await studentCourseService.EnrollAsync(courseId);

        if (studentCourseService.IsValid) return Ok(result);

        return BadRequest(studentCourseService.ToErrorResponse());
    }


    [HttpPost]
    public async Task<IActionResult> UnEnroll(int courseId)
    {
        var result = await studentCourseService.UnEnrollAsync(courseId);

        if (studentCourseService.IsValid) return Ok(result);

        return BadRequest(studentCourseService.ToErrorResponse());
    }

}
