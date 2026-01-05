using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
namespace OnlineCourse.Data.Entites;
[Table("roles")]
[Index(nameof(Code), IsUnique = true)]
public class Role : BaseInfoEntity
{
    [InverseProperty(nameof(User.Role))]
    public List<User>? Users { get; set; }
}