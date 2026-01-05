using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourse.Data.Entites;
[Table("contents")]
public class Content : BaseDateTime
{
    [Column("id")]
    [Key]
    public int Id { get; set; }

    [Column("name")]
    [Required]
    public string Name { get; set; } = string.Empty;

    [Column("url")]
    [Required]
    public string Url { get; set; } = string.Empty;

    [Column("folder_name")]
    [Required]
    public string FolderName { get; set; } = string.Empty;
}
