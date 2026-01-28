using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.Extensions;
using OnlineCourse.Common.Models;
using OnlineCourse.Common.Models.Auth;
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
    public async Task<IActionResult> Register([FromForm] CreateUserModel model)
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

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var result = await authService.GetProfileAsync();
        if (authService.IsValid) return Ok(result);
        return BadRequest(authService.ToErrorResponse());
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateProfile([FromForm]UpdateProfileModel model)
    {
        var result = await authService.UpdateProfileAsync(model);
        if (authService.IsValid) return Ok(result);
        return BadRequest(authService.ToErrorResponse());
    }
}