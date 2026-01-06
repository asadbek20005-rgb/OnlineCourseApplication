using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models;

public class LoginModel
{
    [StringLength(30, ErrorMessage = "Foydalanuvchi nomi 30 ta belgidan oshmasligi kerak")]
    public string? Username { get; set; }

    [StringLength(100, ErrorMessage = "Email 100 ta belgidan oshmasligi kerak")]
    [EmailAddress(ErrorMessage = "Email manzil noto‘g‘ri formatda")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Parol kiritilishi shart")]
    public string Password { get; set; } = string.Empty;
}

