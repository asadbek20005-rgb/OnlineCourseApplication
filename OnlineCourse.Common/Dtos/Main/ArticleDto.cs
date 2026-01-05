namespace OnlineCourse.Common.Dtos;

public class ArticleDto : BaseDateTimeDto
{
    //Keys
    public int Id { get; set; }
    public int PhotoContentId { get; set; }
    public int CategoryId { get; set; }

    // Mains
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
}
