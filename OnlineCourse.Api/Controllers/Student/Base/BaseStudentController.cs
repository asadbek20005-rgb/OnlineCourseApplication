using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineCourse.Api.Controllers.Student;
using static OnlineCourse.Common.Constants.RoleConstants;
[Route("api/student/[controller]/[action]")]
[Authorize(Roles = StudentRole)]
[ApiController]
public class BaseStudentController : ControllerBase
{
}
