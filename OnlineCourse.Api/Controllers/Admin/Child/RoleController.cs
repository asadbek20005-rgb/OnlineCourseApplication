using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.Models;
using OnlineCourse.Common.Models.Info;
using OnlineCourse.Data.Entites;
using OnlineCourse.Service.Admin;

namespace OnlineCourse.Api.Controllers.Admin;

public class RoleController(IBaseInfoService<Role> service) : BaseInfoController<Role, CreateRoleModel, UpdateRoleModel, RoleDto, int>(service)
{
}
