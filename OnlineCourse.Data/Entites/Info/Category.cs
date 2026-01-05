using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourse.Data.Entites;
[Table("categories")]
[Index(nameof(Code), IsUnique = true)]
public class Category : BaseInfoEntity
{
    [InverseProperty(nameof(Course.Category))]
    public List<Course>? Courses { get; set; }

    [InverseProperty(nameof(Article.Category))]
    public List<Article>? Articles { get; set; } 
}
