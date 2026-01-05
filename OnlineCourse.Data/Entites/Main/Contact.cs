using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourse.Data.Entites;
[Table("contacts")]
public class Contact : BaseDateTime
{
    [Column("id")]
    [Key]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(100)]
    [Required]
    public string Title { get; set; } = string.Empty;

    [Column("description")]
    public string? Description { get; set; } = string.Empty;

    [Column("phone_number")]
    [StringLength(15)]
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;

    [Column("email")]
    [StringLength(100)]
    [EmailAddress]
    [Required]
    public string Email { get; set; } = string.Empty;

    [InverseProperty(nameof(Comment.Contact))]
    public List<Comment>? Comments { get; set; }
}