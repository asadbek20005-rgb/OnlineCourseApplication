using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static OnlineCourse.Common.Constants.RoleConstants;
namespace OnlineCourse.Api.Controllers.Instructor;

[Route("api/instructor/[controller]/[action]")]
[ApiController]
[Authorize(Roles = InstructorRole)]
public class BaseInstructorController : ControllerBase
{
}
