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

        unitOfWork.UserRepository().Update(user);
        await unitOfWork.SaveChangesAsync();
        return token;

    }

    public async Task<TokenDto?> RefreshTokenAsync(TokenDto model)
    {
        var (check, username) = jwtService.ValidateAndGetUser(model.accessToken);

        if (!check)
        {
            AddError("Token noto'g'ri yoki yaroqsiz");
            return null;
        }

        var user = await unitOfWork.UserRepository()
            .GetAll().Where(x => x.Username.Equals(username) && x.StatusId == Active)
            .FirstOrDefaultAsync();

        var isNotValid = user is null || user.RefreshToken != model.refreshToken ||
                         user.RefreshTokenExpireTime <= DateTime.Now;

        if (isNotValid)
        {
            AddError("Refresh token amal qilish muddati tugagan yoki noto?g?ri");
            return null;
        }

        var tokenDto = jwtService.GenerateToken(user!, populateExp: false);

        return tokenDto;
    }
}