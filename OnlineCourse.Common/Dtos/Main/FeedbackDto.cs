namespace OnlineCourse.Common.Dtos;

public class FeedbackDto : BaseDateTimeDto
{
    public int Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Job { get; set; } = string.Empty;
}
