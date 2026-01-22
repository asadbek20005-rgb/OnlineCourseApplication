using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models;

public class CreateCourseModel
{
    // Primary and Foreign Keys

    [Required(ErrorMessage = "Kategoriya tanlanishi shart.")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Daraja (level) tanlanishi shart.")]
    public int LevelId { get; set; }

    [Required(ErrorMessage = "Kurs rasmi tanlanishi shart.")]
    public IFormFile PhotoContent { get; set; }


    // Main Properties
    [Required(ErrorMessage = "Kurs nomi kiritilishi shart.")]
    [StringLength(100, ErrorMessage = "Kurs nomi 100 ta belgidan oshmasligi kerak.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Kurs dasturi (curriculum) kiritilishi shart.")]
    public string Curriculum { get; set; } = string.Empty;
}