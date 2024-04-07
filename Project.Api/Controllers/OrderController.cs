using Core.Crud;
using Microsoft.AspNetCore.Mvc;
using Project.Data.DTO;
using Project.Data.Models;
using Project.Services.Interfaces;
using Project.Services.Repositories;

namespace Project.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/order")]
    public class OrderController : CrudControllerBase<Order, OrderDTO, int>
    {


        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger) : base(orderService, logger)
        {
            _logger = logger;
            _orderService = orderService;
        }

        //public override async Task<IActionResult> Update([FromBody] TenantDTO dto, int id, CancellationToken cancellationToken = default)
        //{
        //    return Ok(await _tenantService.Update(dto));
        //}

        //public virtual async Task<IActionResult> Insert([FromBody] TenantDTO dto, CancellationToken cancellationToken = default)
        //{
        //    return Ok(await _tenantService.InsertItem(dto));
        //}
    
    }
}
