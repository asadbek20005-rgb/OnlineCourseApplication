using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourse.Data.Entites;
[Table("content_types")]
[Index(nameof(Code), IsUnique = true)]
public class ContentType : BaseInfoEntity
{
    [InverseProperty(nameof(Content.ContentType))]
    public List<Content>? Contents { get; set; }

    [Column("type_name")]
    public string? TypeName { get; set; }
}
