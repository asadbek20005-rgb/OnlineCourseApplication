using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourse.Data.Entites;

public abstract class BaseInfoEntity : BaseDateTime
{
    [Column("id")]
    [Key]
    public int Id { get; set; }

    [Column("full_name")]
    [StringLength(50)]
    [Required]
    public string FullName { get; set; } = string.Empty;

    [Column("short_name")]
    [StringLength(50)]
    [Required]
    public string ShortName {  get; set; } = string.Empty;

    [Column("code")]
    [Required]
    public int Code { get; set; }
}
