using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models;

public class CreateBaseInfoModel : CreateBaseDateTimeModel
{
    [Required(ErrorMessage = "To‘liq nom kiritilishi shart.")]
    [StringLength(50, ErrorMessage = "To‘liq nom 50 ta belgidan oshmasligi kerak.")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Qisqa nom kiritilishi shart.")]
    [StringLength(50, ErrorMessage = "Qisqa nom 50 ta belgidan oshmasligi kerak.")]
    public string ShortName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Kod kiritilishi shart.")]
    public int Code { get; set; }
}
