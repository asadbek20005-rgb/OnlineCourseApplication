using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models;

public class CreateLessonModel
{
    // Keys

    [Required]
    public IFormFile File { get; set; }
    // Main
    [Required(ErrorMessage = "Dars nomi kiritilishi shart.")]
    [StringLength(100, ErrorMessage = "Dars nomi 100 ta belgidan oshmasligi kerak.")]
    public string Title { get; set; } = null!;

    public string? Description { get; set; } = string.Empty;
}   