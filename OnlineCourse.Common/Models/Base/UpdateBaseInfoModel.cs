using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models;

public class UpdateBaseInfoModel : UpdateBaseDateTimeModel
{
    [StringLength(50)]
    public string? FullName { get; set; } = string.Empty;

    [StringLength(50)]
    public string? ShortName { get; set; } = string.Empty;

    public int? Code { get; set; }
}
