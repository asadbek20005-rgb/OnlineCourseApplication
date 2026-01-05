using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourse.Data.Entites;
[Table("statuses")]
[Index(nameof(Code), IsUnique = true)]
public class Status : BaseInfoEntity
{
    [InverseProperty(nameof(User.Status))]
    public List<User>? Users { get; set; }

    [InverseProperty(nameof(Course.Status))]
    public List<Course>? Courses { get; set; }
}

