using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourse.Data.Entites;
[Table("faqs")]
public class Faq : BaseDateTime
{
    [Column("id")]
    [Key]
    public int Id { get; set; }

    [Column("question")]
    [Required]
    public string Question { get; set; } = string.Empty;

    [Column("answer")]
    [Required]
    public string Answer {  get; set; } = string.Empty;
}
