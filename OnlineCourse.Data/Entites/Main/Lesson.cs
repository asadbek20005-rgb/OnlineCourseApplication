using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourse.Data.Entites;
[Table("lessons")]
public class Lesson : BaseDateTime
{
    // Keys
    [Column("id")]
    [Key]
    public int Id { get; set; }

    [Column("course_id")]
    public int CourseId { get; set; }

    [Column("status_id")]
    public int StatusId { get; set; }


    // Main 

    [Column("title")]
    [StringLength(100)]
    [Required]
    public string Title { get; set; } = string.Empty;

    [Column("description")]
    public string? Description { get; set; } = string.Empty;

    // Navigation 

    [ForeignKey(nameof(CourseId))]
    public Course Course { get; set; } = null!;

    [ForeignKey(nameof(StatusId))]
    public Status Status { get; set; } = null!;

    [InverseProperty(nameof(Content.Lesson))]
    public List<Content>? Contents { get; set; } 

}
