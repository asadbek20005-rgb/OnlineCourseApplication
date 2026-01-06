using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.Models;
using OnlineCourse.Data.Entites;
using OnlineCourse.Data.Repositories;
using OnlineCourse.Service.Helpers;
using StatusGeneric;

namespace OnlineCourse.Service.Public;
using static OnlineCourse.Common.Constants.StatusConstants;
public class AuthService(IUnitOfWork unitOfWork, IJwtService jwtService) : StatusGenericHandler, IAuthService
{
    public async Task<TokenDto?> LoginAsync(LoginModel model)
    {
        var user = await unitOfWork.UserRepository()
            .GetAll()
            .Where(x => (x.Username == model.Username || x.Email == model.Email) && x.StatusId == Active)
            .FirstOrDefaultAsync();

        if (user is null)
        {
            AddError("Foydalanuvchi topilmadi");
            return null;
        }



        var passwordHasher = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, model.Password);
        if (passwordHasher == PasswordVerificationResult.Failed)
        {
            AddError("Foydalanuvchining paroli noto'g'ri ");
            return null;
        }

        var token = jwtService.GenerateToken(user, true);

        return token;

    }
}