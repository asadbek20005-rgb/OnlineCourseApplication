using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models;

public class UpdateCourseModel : UpdateBaseDateTimeModel
{
    // Primary and Foreign Keys

    public int StatusId { get; set; }
    public int CategoryId { get; set; }
    public int PhotoContentId { get; set; }
    public int LevelId { get; set; }

    // Main Properites

    [StringLength(100)]
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Curriculum { get; set; } = string.Empty;
}
