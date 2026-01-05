using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourse.Common.Dtos;

public class CourseDto : BaseDateTimeDto
{
    // Primary and Foreign Keys
    public int Id { get; set; }
    public int StatusId { get; set; }
    public int CategoryId { get; set; }
    public int PhotoContentId { get; set; }
    public int LevelId { get; set; }

    // Main Properites

    public string Title { get; set; } = string.Empty;
    public string Curriculum { get; set; } = string.Empty;
}
