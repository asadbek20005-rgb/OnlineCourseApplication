using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourse.Data.Entites;
[Table("courses")]
public class Course : BaseDateTime
{
    // Primary and Foreign Keys

    [Column("id")]
    [Key]
    public int Id { get; set; }

    [Column("status_id")]
    public int StatusId { get; set; }

    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("photo_content_id")]
    public int PhotoContentId { get; set; }

    [Column("level_id")]
    public int LevelId { get; set; }

    // Main Properites

    [Column("title")]
    [StringLength(100)]
    [Required]
    public string Title { get; set; } = string.Empty;

    [Column("curriculum")]
    [Required]
    public string Curriculum { get; set; } = string.Empty;

    [Column("rating")]
    public decimal? Rating { get; set; }

    // Navigation Properties

    [ForeignKey(nameof(StatusId))]
    public Status Status { get; set; } = null!;

    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; } = null!;

    [ForeignKey(nameof(PhotoContentId))]
    public Content Content { get; set; } = null!;

    [InverseProperty(nameof(UserCourse.Course))]
    public List<UserCourse>? UserCourses { get; set; }

    [ForeignKey(nameof(LevelId))]
    public Level Level { get; set; } = null!;

    [InverseProperty(nameof(Lesson.Course))]
    public List<Lesson>? Lessons { get; set; }

    [InverseProperty(nameof(Comment.Course))]
    public List<Comment>? Comments { get; set; }

    [InverseProperty(nameof(Review.Course))]
    public List<Review>? Reviews { get; set; } 

}