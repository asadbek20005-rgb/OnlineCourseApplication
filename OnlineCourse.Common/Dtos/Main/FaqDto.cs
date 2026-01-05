namespace OnlineCourse.Common.Dtos;

public class FaqDto : BaseDateTimeDto
{
    public int Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
}
