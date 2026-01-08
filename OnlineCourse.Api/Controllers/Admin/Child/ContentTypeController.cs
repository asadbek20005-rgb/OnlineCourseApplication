using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.Models;
using OnlineCourse.Service.Admin;
using OnlineCourse.Data.Entites;

namespace OnlineCourse.Api.Controllers.Admin;

public class ContentTypeController(IBaseInfoService<ContentType> service) : BaseInfoController<ContentType, CreateContentTypeModel, UpdateContentTypeModel, ContentTypeDto, int>(service)
{
}
