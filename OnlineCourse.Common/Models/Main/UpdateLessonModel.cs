using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models;

public class UpdateLessonModel
{
    [StringLength(100)]
    public string? Title { get; set; }

    public string? Description { get; set; }

    public IFormFile? File { get; set; }
}