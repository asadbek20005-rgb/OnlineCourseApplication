namespace OnlineCourse.Common.Dtos;

public class BaseDateTimeDto
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid CreatedUserId { get; set; }
    public Guid? UpdatedUserId { get; set; }
}
