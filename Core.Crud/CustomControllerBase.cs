using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Core.Crud.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core.Crud
{
    /// <summary>
    /// Represents the class of base controller
    /// </summary>
    public abstract class CustomControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        protected readonly ILogger<CustomControllerBase> Logger;

        protected CustomControllerBase(ILogger<CustomControllerBase> logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// Returns the URL consisting of the current domain and the specified tail.
        /// </summary>
        /// <param name="tail">The tail which should be added to the end of the URL.</param>
        protected string CalculateUrl(string tail) =>
            Request.Scheme + "://" + Request.Host + "/" + tail;

        protected async Task<IActionResult> NoContent(Func<Task> action)
        {
            await action();
            return NoContent();
        }

        protected async Task<IActionResult> GetOkResult<T>(Func<Task<T>> action)
        {
            return Ok(await action());
        }

        protected CreatedResult Created<TEntityDTO, TKey>(TEntityDTO result) where TEntityDTO: IDTO<TKey> where TKey: IEquatable<TKey> 
        {
            var idStringValue = Convert.ToString(result.Id);
            return Created(
                $"{Request.Scheme}://{Request.Host.Value}{Request.Path}/{idStringValue}",
                result);
        }
    }
}