using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models;

public class UpdateCourseModel
{
    // Primary and Foreign Keys
    public int? CategoryId { get; set; }
    public int? LevelId { get; set; }

    // Main Properites

    [StringLength(100)]
    public string? Title { get; set; }


    public string? Curriculum { get; set; }
}