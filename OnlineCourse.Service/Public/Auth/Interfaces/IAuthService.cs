using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.Models;
using StatusGeneric;

namespace OnlineCourse.Service.Public;

public interface IAuthService : IStatusGeneric
{
    Task<TokenDto?> LoginAsync(LoginModel model);
}
