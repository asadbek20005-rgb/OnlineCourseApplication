using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models;

public class CreateLessonModel : CreateBaseDateTimeModel
{
    // Keys
    [Required(ErrorMessage = "Kurs tanlanishi shart.")]
    public int CourseId { get; set; }

    [Required]
    public IFormFile VideoContent { get; set; }
    // Main
    [Required(ErrorMessage = "Dars nomi kiritilishi shart.")]
    [StringLength(100, ErrorMessage = "Dars nomi 100 ta belgidan oshmasligi kerak.")]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;
}
