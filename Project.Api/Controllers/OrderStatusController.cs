using Core.Crud;
using Microsoft.AspNetCore.Mvc;
using Project.Data.DTO;
using Project.Data.Models;
using Project.Services.Interfaces;

namespace Project.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/order-status")]
    public class OrderStatusController : CrudControllerBase<OrderStatus, OrderStatusDTO, int>
    {
        private readonly ILogger<OrderStatusController> _logger;
        private readonly IOrderStatusService _orderStatusService;

        public OrderStatusController(IOrderStatusService orderStatusService, ILogger<OrderStatusController> logger) : base(orderStatusService, logger)
        {
            _logger = logger;
            _orderStatusService = orderStatusService;
        }

        [HttpGet, Route("dropdown")]
        public virtual async Task<IActionResult> Get( CancellationToken cancellationToken = default)
        {
            return Ok(await _orderStatusService.GetDropdownItems());
        }
    }
}
