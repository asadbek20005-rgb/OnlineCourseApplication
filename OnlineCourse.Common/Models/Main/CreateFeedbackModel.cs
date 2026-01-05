using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models;

public class CreateFeedbackModel : CreateBaseDateTimeModel
{
    [Required(ErrorMessage = "Xabar matni kiritilishi shart.")]
    public string Message { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ism kiritilishi shart.")]
    [StringLength(150, ErrorMessage = "Ism 150 ta belgidan oshmasligi kerak.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lavozim kiritilishi shart.")]
    [StringLength(150, ErrorMessage = "Lavozim 150 ta belgidan oshmasligi kerak.")]
    public string Job { get; set; } = string.Empty;
}