using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models;

public class UpdateBaseInfoModel 
{
    [StringLength(50)]
    public string? FullName { get; set; }

    [StringLength(50)]
    public string? ShortName { get; set; } 

    public int? Code { get; set; }
}
