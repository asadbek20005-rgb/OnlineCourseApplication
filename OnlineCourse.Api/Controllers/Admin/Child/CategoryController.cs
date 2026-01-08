using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.Models;
using OnlineCourse.Data.Entites;
using OnlineCourse.Service.Admin;

namespace OnlineCourse.Api.Controllers.Admin;

public class CategoryController(IBaseInfoService<Category> service) : BaseInfoController<Category, CreateCategoryModel, UpdateCategoryModel, CategoryDto, int>(service)
{
}
