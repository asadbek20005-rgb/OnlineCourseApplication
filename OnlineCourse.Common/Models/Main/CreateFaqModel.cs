using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models;

public class CreateFaqModel : CreateBaseDateTimeModel
{
    [Required(ErrorMessage = "Savol kiritilishi shart.")]
    public string Question { get; set; } = string.Empty;

    [Required(ErrorMessage = "Javob kiritilishi shart.")]
    public string Answer { get; set; } = string.Empty;
}