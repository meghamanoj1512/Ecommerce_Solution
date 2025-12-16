using Ecommerce_Solution.Domain;
using Ecommerce_Solution.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Solution.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _service;

        public OrdersController(OrderService service)
        {
            _service = service;
        }

        [HttpPost("get-latest-order")]
        public async Task<IActionResult> GetLatestOrder(OrderRequest request)
        {
            var data = await _service.GetLatestOrderAsync(request);

            if (data == null)
                return NotFound("No data found!");

            return Ok(data);

        }
}

}

