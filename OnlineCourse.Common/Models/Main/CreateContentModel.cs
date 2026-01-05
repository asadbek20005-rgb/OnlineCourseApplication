using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models;

public class CreateContentModel : CreateBaseDateTimeModel
{
    // Keys
    [Required(ErrorMessage = "Kontent turi tanlanishi shart.")]
    public int ContentTypeId { get; set; }

    [Required(ErrorMessage = "Dars tanlanishi shart.")]
    public int LessonId { get; set; }

    // Main
    [Required(ErrorMessage = "Kontent nomi kiritilishi shart.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Kontent manzili (URL) kiritilishi shart.")]
    public string Url { get; set; } = string.Empty;

    [Required(ErrorMessage = "Papk? nomi kiritilishi shart.")]
    public string FolderName { get; set; } = string.Empty;
}
