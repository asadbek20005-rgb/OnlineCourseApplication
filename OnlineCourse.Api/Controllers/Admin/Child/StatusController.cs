using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.Models;
using OnlineCourse.Data.Entites;
using OnlineCourse.Service.Admin;

namespace OnlineCourse.Api.Controllers.Admin;

public class StatusController(IBaseInfoService<Status> service) : BaseInfoController<Status, CreateStatusModel, UpdateStatusModel, StatusDto, int>(service)
{
}
