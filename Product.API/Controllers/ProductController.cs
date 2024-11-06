using Microsoft.AspNetCore.Mvc;
using Product.API.Services;

namespace Product.API.Controllers
{
    [ApiController]
    [Route("api/Product")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _Service;

        public ProductController(ProductService orderService)
        {
            _Service = orderService;
        }

        [HttpGet("")]
        public IActionResult GetOrder()
        {
            var orderResponse = _Service.GetAllProduct();
            if (!orderResponse.Any())
            {
                return NotFound();
            }
            return Ok(orderResponse);
        }

    }
}
