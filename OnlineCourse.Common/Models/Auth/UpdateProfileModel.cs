using Microsoft.AspNetCore.Http;

namespace OnlineCourse.Common.Models.Auth;

public class UpdateProfileModel
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Bio { get; set; }
    public string? Gender { get; set; }
    public IFormFile? PhotoContent { get; set; }
}
