using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourse.Data.Entites;
[Table("feedbacks")]
public class Feedback : BaseDateTime
{
    [Column("id")]
    [Key]
    public int Id { get; set; }

    [Column("message")]
    [Required]
    public string Message { get; set; } = string.Empty;

    [Column("name")]
    [StringLength(150)]
    [Required]
    public string Name { get; set; } = string.Empty;


    [Column("job")]
    [StringLength(150)]
    [Required]
    public string Job { get; set; } = string.Empty;
}
