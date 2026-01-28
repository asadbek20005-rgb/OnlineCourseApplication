using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Common.Extensions;
using OnlineCourse.Common.Models;
using OnlineCourse.Service.Student;

namespace OnlineCourse.Api.Controllers.Student;

public class ReviewController(IReviewService reviewService) : BaseStudentController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateReviewModel model)
    {
        var result = await reviewService.CreateReviewAsync(model);
        if (reviewService.IsValid) return Ok(result);

        return BadRequest(reviewService.ToErrorResponse());
    }
}
