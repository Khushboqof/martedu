using MartEdu.Domain.Commons;
using MartEdu.Domain.Configurations;
using MartEdu.Domain.Enums;
using MartEdu.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MartEdu.Api.Controllers
{
    public abstract class GenericController<TService, TSource, TSourceForCreationDto> : ControllerBase
        where TService : IGenericService<TSource, TSourceForCreationDto>
        where TSource : IAuditable
    {
        protected readonly TService service;

        public GenericController(TService service)
        {
            this.service = service;
        }

        public abstract Task<ActionResult<BaseResponse<TSource>>> Create(TSourceForCreationDto creationDto);

        [HttpGet]
        public virtual async Task<ActionResult<BaseResponse<IEnumerable<TSource>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await service.GetAllAsync(@params);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<BaseResponse<TSource>>> Get(Guid id)
        {
            var result = await service.GetAsync(p => p.Id == id);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult<BaseResponse<TSource>>> Update(Guid id, TSourceForCreationDto creationDto)
        {
            var result = await service.UpdateAsync(id, creationDto);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<BaseResponse<TSource>>> Delete(Guid id)
        {
            var result = await service.DeleteAsync(p => p.Id == id && p.State != ItemState.Deleted);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpPost("{id}/restore")]
        public virtual async Task<ActionResult<BaseResponse<TSource>>> Restore(Guid id)
        {
            var result = await service.RestoreAsync(p => p.Id == id);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }
    }
}
