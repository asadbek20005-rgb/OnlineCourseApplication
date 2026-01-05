using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models;

public class CreateArticleModel : CreateBaseDateTimeModel
{
    // Keys
    [Required(ErrorMessage = "Rasm kontenti tanlanishi shart.")]
    public int PhotoContentId { get; set; }

    [Required(ErrorMessage = "Kategoriya tanlanishi shart.")]
    public int CategoryId { get; set; }

    // Mains
    [Required(ErrorMessage = "Maqola sarlavhasi kiritilishi shart.")]
    [StringLength(100, ErrorMessage = "Maqola sarlavhasi 100 ta belgidan oshmasligi kerak.")]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;
}
