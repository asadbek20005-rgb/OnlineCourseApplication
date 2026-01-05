using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models;

public class UpdateBaseDateTimeModel
{
    [Required(ErrorMessage = "Yangilangan sana kiritilishi shart.")]
    public DateTime UpdatedAt { get; set; }

    [Required(ErrorMessage = "Yangilagan foydalanuvchi ID si kiritilishi shart.")]
    public Guid UpdatedUserId { get; set; }
}