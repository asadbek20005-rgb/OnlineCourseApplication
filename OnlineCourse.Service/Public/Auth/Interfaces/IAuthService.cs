using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.Models;
using OnlineCourse.Common.Models.Auth;
using StatusGeneric;

namespace OnlineCourse.Service.Public;

public interface IAuthService : IStatusGeneric
{
    Task<TokenDto?> LoginAsync(LoginModel model);
    Task<TokenDto?> RefreshTokenAsync(TokenDto model);
    Task<string?> RegisterAsync(CreateUserModel model);
    Task<UserDto?> GetProfileAsync();
    Task<string?> UpdateProfileAsync(UpdateProfileModel model);
    Task<string?> ForgotPassword();
}
