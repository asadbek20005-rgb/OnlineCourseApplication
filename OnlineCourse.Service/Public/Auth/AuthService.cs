using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.Extensions;
using OnlineCourse.Common.Models;
using OnlineCourse.Data.Entites;
using OnlineCourse.Data.Repositories;
using OnlineCourse.Service.Helpers;
using StatusGeneric;
using System.Text.RegularExpressions;

namespace OnlineCourse.Service.Public;
using static OnlineCourse.Common.Constants.GenderConstants;
using static OnlineCourse.Common.Constants.MinioFolderConstant;
using static OnlineCourse.Common.Constants.RoleConstants;
using static OnlineCourse.Common.Constants.StatusConstants;
public class AuthService(IUnitOfWork unitOfWork, IJwtService jwtService, IContentService contentService) : StatusGenericHandler, IAuthService
{
    public async Task<TokenDto?> LoginAsync(LoginModel model)
    {
        bool isValid = ValidateEmailOrUsername(model.Identifier);
        if (!isValid) return null;
        var user = await unitOfWork.UserRepository()
            .GetAll(x => x.Role)
            .Where(x => (x.Username == model.Identifier || x.Email == model.Identifier) && x.StatusId == Active)
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

    public async Task<string?> RegisterAsync(CreateUserModel model)
    {
        await using var transaction = unitOfWork.BeginTransaction();
        try
        {
            if (!IsValidRole(model.RoleId)) return null;
            if (!await IsUserExistsAsync(model.Username, model.Email)) return null;

            var newUser = model.MapToEntity<User, CreateUserModel>();
            newUser.StatusId = Active;
            newUser.PhotoContentId = await contentService.CreateContentForImage(model.PhotoContent, UserImages);
            CombineStatuses(contentService);
            if (contentService.HasErrors) return null;

            newUser.PasswordHash = new PasswordHasher<User>().HashPassword(newUser, model.Password);
            newUser.Gender = model.Gender == 1 ? Male : Female;
            newUser.CreatedAt = DateTime.UtcNow;

            await unitOfWork.UserRepository().AddAsync(newUser);
            await unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();
            return "Foydalanuvchi muvaffaqiyatli ro‘yxatdan o‘tdi!";

        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception(ex.Message);
        }
    }

    private bool IsValidRole(int roleId)
    {
        if (roleId != InstructorRoleId && roleId != StudentRoleId)
        {
            AddError("Rol noto‘g‘ri. Iltimos, Instructor yoki Student rolini tanlang.");
            return false;
        }

        return true;
    }

    private async Task<bool> IsUserExistsAsync(string username, string email)
    {
        bool isExist = await unitOfWork.UserRepository()
            .GetAll()
            .Where(x => x.Username.Equals(username) || x.Email.Equals(email))
            .AnyAsync();


        if (isExist)
        {
            AddError("Bunday foydalanuvchi nomi yoki email allaqachon mavjud");
            return false;
        }

        return true;
    }
    private bool ValidateEmailOrUsername(string emailOrUsername)
    {
        if (string.IsNullOrWhiteSpace(emailOrUsername))
        {
            AddError("Username yoki Email kiritilishi shart");
            return false;
        }

        // Email bo‘lsa
        if (emailOrUsername.Contains('@'))
        {
            var result = IsValidEmail(emailOrUsername);
            if (!result)
            {
                AddError("Email formati noto‘g‘ri");
                return false;
            }
        }
        // Username bo‘lsa
        else
        {
            var result = IsValidUsername(emailOrUsername);
            if (!result)
            {
                AddError("Username noto‘g‘ri formatda (3–30 belgi, faqat harf va raqam)");
                return false;
            }
        }

        return true;
    }
    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
    private static bool IsValidUsername(string username)
    {
        // 3–30 ta belgi, faqat harf, raqam va underscore
        return Regex.IsMatch(username, @"^[a-zA-Z0-9_]{3,30}$");
    }

}