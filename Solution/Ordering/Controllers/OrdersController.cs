using Microsoft.AspNetCore.Mvc;
using OrdersApi.Persistence;
using System;
using System.Threading.Tasks;

namespace OrdersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _orderRepository.GetOrdersAsync();
            return Ok(data);
        }

        [HttpGet]
        [Route("{orderId}",Name ="GetByOrderId")]
        public async Task<IActionResult> GetOrderById(string orderId)
        {
            if(!Guid.TryParse(orderId,out Guid orederIdGuid))
                return BadRequest();
            var order = await _orderRepository.GetOrderAsync(orederIdGuid);
            if(order == null)
                return NotFound();
            return Ok(order);
        }
    }
}
