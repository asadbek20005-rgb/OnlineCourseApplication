namespace OnlineCourse.Common.Dtos;

public class BaseInfoDto : BaseDateTimeDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public int Code { get; set; }
}