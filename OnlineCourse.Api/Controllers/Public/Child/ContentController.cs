using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Common.Extensions;
using OnlineCourse.Service.Helpers;

namespace OnlineCourse.Api.Controllers.Public;

public class ContentController(IContentService service) : BasePublicController
{
    [HttpGet("{filePath}")]
    public async Task<IActionResult> DownloadFile(string filePath)
    {
        var (result, contentType) = await service.DownloadFileAsync(filePath);
        if (service.IsValid) return File(result, contentType);
        return BadRequest(service.ToErrorResponse());
    }
}