using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourse.Data.Entites;
[Table("contents")]
public class Content : BaseDateTime
{
    // Keys
    [Column("id")]
    [Key]
    public int Id { get; set; }

    [Column("content_type_id")]
    public int ContentTypeId { get; set; }

    [Column("lesson_id")]
    public int LessonId { get; set; }

    // Main

    [Column("name")]
    [Required]
    public string Name { get; set; } = string.Empty;

    [Column("url")]
    [Required]
    public string Url { get; set; } = string.Empty;

    [Column("folder_name")]
    [Required]
    public string FolderName { get; set; } = string.Empty;

    // Navigation

    [ForeignKey(nameof(ContentTypeId))]
    public ContentType ContentType { get; set; } = null!;

    [ForeignKey(nameof(LessonId))]
    public Lesson Lesson { get; set; } = null!;
}