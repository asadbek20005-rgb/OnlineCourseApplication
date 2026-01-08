using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace OnlineCourse.Service.Helpers;

public class UserHelper(IHttpContextAccessor httpContextAccessor) : IUserHelper
{
    public string GetUserId() => httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

}
