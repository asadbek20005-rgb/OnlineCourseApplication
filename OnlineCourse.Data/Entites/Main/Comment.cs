using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourse.Data.Entites;
[Table("comments")]
public class Comment : BaseDateTime
{

    // Keys
    [Column("id")]
    [Key]
    public int Id { get; set; }

    [Column("comment_reply_id")]
    public int CommentReplyId { get; set; }

    [Column("article_id")]
    public int ArticleId { get; set; }

    [Column("course_id")]
    public int CourseId { get; set; }

    [Column("contact_id")]
    public int ContactId { get; set; }

    // Mains

    [Column("email")]
    [StringLength(100)]
    [EmailAddress]
    [Required]
    public string Email { get; set; } = string.Empty;

    [Column("name")]
    [StringLength(150)]
    [Required]
    public string Name { get; set; } = string.Empty;

    [Column("text")]
    [Required]
    public string Text { get; set; } = string.Empty;

    //Navigations

    [ForeignKey(nameof(CommentReplyId))]
    public Comment CommentReply { get; set; } = null!;

    [ForeignKey(nameof(ArticleId))]
    public Article Article { get; set; } = null!;

    [ForeignKey(nameof(CourseId))]
    public Course Course { get; set; } = null!;

    [ForeignKey(nameof(ContactId))]
    public Contact Contact { get; set; } = null!;

}
