using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniEcommerce.Core.DTOs;
using MiniEcommerce.Core.ServicesAbstraction;

namespace MiniEcommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService) => _orderService = orderService;

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateDto dto)
        {
            try
            {
                return Ok(await _orderService.PlaceOrderAsync(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            return order == null ? NotFound() : Ok(order);
        }
    }
}
