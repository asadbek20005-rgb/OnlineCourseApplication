using Microsoft.AspNetCore.Http;
using OnlineCourse.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models;

public class CreateUserModel 
{
    // Primary and Foreign Keys
    [Required(ErrorMessage = "Rol tanlanishi shart")]
    public int RoleId { get; set; } // Instructor or Student

    public IFormFile? PhotoContent { get; set; }

    // Main Properties

    [Required(ErrorMessage = "Ism kiritilishi shart")]
    [StringLength(150, ErrorMessage = "Ism 150 ta belgidan oshmasligi kerak")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Familiya kiritilishi shart")]
    [StringLength(150, ErrorMessage = "Familiya 150 ta belgidan oshmasligi kerak")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Foydalanuvchi nomi kiritilishi shart")]
    [StringLength(30, ErrorMessage = "Foydalanuvchi nomi 30 ta belgidan oshmasligi kerak")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email manzil kiritilishi shart")]
    [EmailAddress(ErrorMessage = "Email manzil noto‘g‘ri formatda")]
    [StringLength(100, ErrorMessage = "Email 100 ta belgidan oshmasligi kerak")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Parol kiritilishi shart")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Parolni tasdiqlash shart")]
    [Compare(nameof(Password), ErrorMessage = "Parollar bir-biriga mos emas")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Bio 500 ta belgidan oshmasligi kerak")]
    public string? Bio { get; set; }
    [Required(ErrorMessage = "Jins tanlanishi shart")]
    [Range(1, 2, ErrorMessage = "Jins noto‘g‘ri tanlangan. Iltimos, Erkak yoki Ayolni tanlang.")]
    public int Gender { get; set; }

}