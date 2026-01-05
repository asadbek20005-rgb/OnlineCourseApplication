namespace OnlineCourse.Common.Dtos;

public class CommentDto : BaseDateTimeDto
{
    // Keys
    public int Id { get; set; }
    public int CommentReplyId { get; set; }
    public int ArticleId { get; set; }
    public int CourseId { get; set; }
    public int ContactId { get; set; }

    // Mains

    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}
