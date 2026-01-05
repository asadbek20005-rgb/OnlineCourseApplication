namespace OnlineCourse.Common.Dtos;

public class ContentDto : BaseDateTimeDto
{
    // Keys
    public int Id { get; set; }
    public int ContentTypeId { get; set; }
    public int LessonId { get; set; }

    // Main

    public string Name { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public string FolderName { get; set; } = string.Empty;
}
