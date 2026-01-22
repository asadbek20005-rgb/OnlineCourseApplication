using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Service.Public;

namespace OnlineCourse.Api.Controllers.Public;

public class HomeController(IHomeService homeService) : BasePublicController
{
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var result = await homeService.GetTopCategories();
        return Ok(result);
    }


    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        var result = await homeService.GetFeateredCourses();
        return Ok(result);
    }
}
