using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Common.Dtos;
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

    [HttpPost]
    public async Task<IActionResult> Register([FromForm]CreateUserModel model)
    {
        var result = await authService.RegisterAsync(model);

        if (authService.IsValid) return Ok(result);

        return BadRequest(authService.ToErrorResponse());
    }

    [HttpPost]
    public async Task<IActionResult> RefreshToken(TokenDto model)
    {
        var result = await authService.RefreshTokenAsync(model);

        if (authService.IsValid) return Ok(result);

        return BadRequest(authService.ToErrorResponse());
    }
}
