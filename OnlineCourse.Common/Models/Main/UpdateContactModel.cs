using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models;
public class UpdateContactModel : UpdateBaseDateTimeModel
{
    [StringLength(100, ErrorMessage = "Sarlavha 100 ta belgidan oshmasligi kerak.")]
    public string? Title { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    [StringLength(15, ErrorMessage = "Telefon raqami 15 ta belgidan oshmasligi kerak.")]
    public string? PhoneNumber { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "Email manzil 100 ta belgidan oshmasligi kerak.")]
    [EmailAddress(ErrorMessage = "Email manzil noto‘g‘ri formatda.")]
    public string? Email { get; set; } = string.Empty;
}