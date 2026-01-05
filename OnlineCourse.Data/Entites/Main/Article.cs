using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourse.Data.Entites;
[Table("articles")]
public class Article : BaseDateTime
{
    //Keys

    [Column("id")]
    [Key]
    public int Id { get; set; }

    [Column("photo_content_id")]
    public int PhotoContentId { get; set; }

    [Column("category_id")]
    public int CategoryId { get; set; }

    // Mains


    [Column("title")]
    [StringLength(100)]
    [Required]
    public string Title { get; set; } = string.Empty;

    [Column("description")]
    public string? Description { get; set; } = string.Empty;


    // Navigations

    [ForeignKey(nameof(PhotoContentId))]
    public Content PhotoContent { get; set; } = null!;

    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; } = null!;

    [InverseProperty(nameof(Comment.Article))]
    public List<Comment>? Comments { get; set; } = null!;

}
