using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.OrderModel;

namespace Presentation
{
    //Api Controller
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost] //POST: /api/Orders
        public async Task<IActionResult> CreateOrder(OrderRequestDto request)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.orderService.CreateOrderAsync(request, email);
            return Ok(result);
        }

        [HttpGet] //GET: /api/Orders
        public async Task<IActionResult> GetOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.orderService.GetOrdersByUserEmailAsync(email);
            return Ok(result);
        }

        [HttpGet("{id}")] //GET: /api/Orders/id
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var result = await serviceManager.orderService.GetOrderByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("DeliveryMethod")] //GET: /api/Orders/DeliveryMethod
        public async Task<IActionResult> GetAllDeliveryMethod()
        {
            var result = await serviceManager.orderService.GetAllDeliveryMethods();
            return Ok(result);
        }
    }
}
