using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourse.Data.Entites;
[Table("levels")]
[Index(nameof(Code), IsUnique = true)]
public class Level : BaseInfoEntity
{
    [InverseProperty(nameof(Course.Level))]
    public List<Course>? Courses { get; set; }
}
