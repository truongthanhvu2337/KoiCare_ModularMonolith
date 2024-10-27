using Microsoft.AspNetCore.Mvc;
using Order.API.Models;
using Order.API.Services;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetOrder()
        {
            var orderResponse = await _orderService.GetAll();
            if (orderResponse.data == null)
            {
                return NotFound();
            }
            return Ok(orderResponse);
        }

        [HttpPost]
        public async Task<ActionResult<OrderResponse>> CreateOrder([FromBody] Models.Order order)
        {
            if (order == null)
            {
                return BadRequest("invalid data");
            }

            var createdOrderResponse = await _orderService.CreateOrder(order);
            return createdOrderResponse.status != 200 ? createdOrderResponse : StatusCode((int)createdOrderResponse.status, createdOrderResponse);
        }
    }
}
