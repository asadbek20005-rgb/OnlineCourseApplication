using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourse.Data.Entites;
[Table("user_courses")]
public class UserCourse : BaseDateTime
{
    [Column("id")]
    [Key]
    public int Id { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("course_id")]
    public int CourseId { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; } = false;


    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    [ForeignKey(nameof(CourseId))]
    public Course Course { get; set; } = null!;

}
