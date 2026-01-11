namespace OnlineCourse.Common.Dtos;

public class LessonDto : BaseDateTimeDto
{
    // Keys
    public int Id { get; set; }
    public int CourseId { get; set; }

    // Main 

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string StatusName { get; set; } = null!;
    public string VideoContentUrl { get; set; } = null!;
}
