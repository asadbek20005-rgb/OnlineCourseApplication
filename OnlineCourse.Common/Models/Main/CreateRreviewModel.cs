using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models;

public class CreateReviewModel 
{
    // Keys

    [Required(ErrorMessage = "Kurs tanlanishi shart.")]
    public int CourseId { get; set; }

    // Main
    [Required(ErrorMessage = "Sharh matni kiritilishi shart.")]
    public string Message { get; set; } = string.Empty;

    [Required(ErrorMessage = "Baholash kiritilishi shart.")]
    [Range(1, 5, ErrorMessage = "Baholash 1 dan 5 gacha bo‘lishi kerak.")]
    public int Rating { get; set; } // 1–5
}
