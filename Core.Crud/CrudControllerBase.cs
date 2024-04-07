using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Crud.Data;
using Core.Crud.DTO;
using Core.Crud.Exceptions;
using Core.Crud.Filters;
using Core.Crud.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core.Crud
{
    [ApiController]
    public abstract class CrudControllerBase<TEntity, TEntityDTO, TKey> : CustomControllerBase
        where TEntity: class, IEntity<TKey>
        where TEntityDTO : class
        where TKey : IEquatable<TKey>
    {
        
        public  ICrudService<TEntity, TEntityDTO, TKey> _crudService { get; }

        public CrudControllerBase(
            ICrudService<TEntity, TEntityDTO, TKey> crudService,
            ILogger<CrudControllerBase<TEntity, TEntityDTO, TKey>> logger) : base(logger)
        {
            _crudService = crudService;
        }


        [HttpGet, Route("page")]
        public virtual async Task<IActionResult> GetPage([FromQuery] FilterCommand command, CancellationToken cancellationToken)
        {
            return Ok(await _crudService.GetPagedData(command));
        }

        [HttpGet, Route("{id}")]
        public virtual async Task<IActionResult> Get(TKey id, CancellationToken cancellationToken = default)
        {
            return Ok(await _crudService.Get(id));
        }

        [HttpPost]
        public virtual async Task<IActionResult> Insert([FromBody] TEntityDTO dto, CancellationToken cancellationToken = default)
        {
            return Ok(await _crudService.Insert(dto));
        }

        [HttpPut, Route("{id}")]
        public virtual async Task<IActionResult> Update([FromBody] TEntity entity, TKey id, CancellationToken cancellationToken = default)
        {
            return Ok(await _crudService.Update(entity));
        }

        [HttpDelete, Route("{id}")]
        public virtual async Task<IActionResult> Delete(TKey id, CancellationToken cancellationToken = default)
        {
            return Ok(await _crudService.Delete(id));
        }    

    }
 
}