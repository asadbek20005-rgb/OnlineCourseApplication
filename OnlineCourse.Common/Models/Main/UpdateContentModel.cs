namespace OnlineCourse.Common.Models;

public class UpdateContentModel : UpdateBaseDateTimeModel
{
    public int? ContentTypeId { get; set; }
    public int? LessonId { get; set; }
    // Main
    public string? Name { get; set; } = string.Empty;

    public string? Url { get; set; } = string.Empty;

    public string? FolderName { get; set; } = string.Empty;
}