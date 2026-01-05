using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourse.Data.Entites;
[Table("content_types")]
[Index(nameof(Code), IsUnique = true)]
public class ContentType : BaseInfoEntity
{
}
