using OnlineCourse.Common.Dtos;
using OnlineCourse.Data.Entites;

namespace OnlineCourse.Service.Helpers;

public interface IJwtService
{
    TokenDto GenerateToken(User user, bool populateExp);
    Tuple<bool, string> ValidateAndGetUser(string accessToken); 
}
