using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourse.Data.Entites;
[Table("reviews")]
public class Review : BaseDateTime
{
    [Column("id")]
    [Key]
    public int Id { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("course_id")]
    public int CourseId { get; set; }

    // Main

    [Column("message")]
    [Required]
    public string Message { get; set; } = string.Empty;

    [Column("rating")]
    [Required]
    public int Rating { get; set; } // 1-5 



    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    [ForeignKey(nameof(CourseId))]
    public Course Course { get; set; } = null!;
}
