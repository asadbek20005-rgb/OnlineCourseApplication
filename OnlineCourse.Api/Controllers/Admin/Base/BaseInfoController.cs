using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Common.Extensions;
using OnlineCourse.Service.Admin;

namespace OnlineCourse.Api.Controllers.Admin;
using static OnlineCourse.Common.Constants.RoleConstants;
[Authorize(Roles = AdminRole)]
public abstract class BaseInfoController<TEntity, TCreateModel, TUpdateModel, TDto, TId>(IBaseInfoService<TEntity> service) : BaseAdminController
    where TEntity : class
{

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = service.GetAll<TDto>();
        if (service.IsValid)
            return Ok(result);

        return BadRequest(service.ToErrorResponse());
    }

    [HttpGet]
    public async Task<IActionResult> GetById(TId id)
    {
        var result = await service.GetByIdAsync<TDto, TId>(id);
        if (service.IsValid)
            return Ok(result);

        return BadRequest(service.ToErrorResponse());
    }

    [HttpPost]
    public async Task<IActionResult> Create(TCreateModel model)
    {
        var result = await service.CreateAsync(model);
        if (service.IsValid)
            return Ok(result);

        return BadRequest(service.ToErrorResponse());
    }

    [HttpPut]
    public async Task<IActionResult> Update(TId id, TUpdateModel model)
    {
        var result = await service.UpdateAsync(id, model);
        if (service.IsValid)
            return Ok(result);

        return BadRequest(service.ToErrorResponse());
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(TId id)
    {
        var result = await service.DeleteByIdAsync(id);
        if (service.IsValid)
            return Ok(result);

        return BadRequest(service.ToErrorResponse());
    }
}