using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.Models;
using OnlineCourse.Data.Entites;
using OnlineCourse.Service.Admin;

namespace OnlineCourse.Api.Controllers.Admin;

public class LevelController(IBaseInfoService<Level> service) : BaseInfoController<Level, CreateLevelModel, UpdateLevelModel, LevelDto, int>(service)
{
}
