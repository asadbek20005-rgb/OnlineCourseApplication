using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models;
public class CreateBaseDateTimeModel
{
    [Required(ErrorMessage = "Yaratilgan sana kiritilishi shart.")]
    public DateTime CreatedAt { get; set; }

    [Required(ErrorMessage = "Yaratgan foydalanuvchi ID si kiritilishi shart.")]
    public Guid CreatedUserId { get; set; }
}

