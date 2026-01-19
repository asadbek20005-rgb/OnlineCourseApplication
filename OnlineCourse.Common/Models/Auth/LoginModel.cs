using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Username yoki Email kiritilishi shart")]
    public string Identifier { get; set; } = string.Empty;

    [Required(ErrorMessage = "Parol kiritilishi shart")]
    public string Password { get; set; } = string.Empty;
}

