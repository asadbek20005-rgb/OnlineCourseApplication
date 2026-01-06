using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Common.Extensions;
using OnlineCourse.Common.Models;
using OnlineCourse.Service.Public;

namespace OnlineCourse.Api.Controllers.Public;

public class AuthController(IAuthService authService) : BasePublicController
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        var result = await authService.LoginAsync(model);

        if (authService.IsValid) return Ok(result);

        return BadRequest(authService.ToErrorResponse());
    }
}
