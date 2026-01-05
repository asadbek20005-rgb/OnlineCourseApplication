using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models.Main;

public class UpdateArticleModel : UpdateBaseDateTimeModel
{
    // Keys
    public int? PhotoContentId { get; set; }

    public int? CategoryId { get; set; }

    // Mainss
    [StringLength(100, ErrorMessage = "Maqola sarlavhasi 100 ta belgidan oshmasligi kerak.")]
    public string? Title { get; set; }

    public string? Description { get; set; } 
}
