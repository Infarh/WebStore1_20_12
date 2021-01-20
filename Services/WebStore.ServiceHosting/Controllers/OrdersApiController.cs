using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Domain.DTO.Orders;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebAPI.Orders)]
    [ApiController]
    public class OrdersApiController : ControllerBase, IOrderService
    {
        private readonly IOrderService _OrderService;
        private readonly ILogger<OrdersApiController> _Logger;

        public OrdersApiController(IOrderService OrderService, ILogger<OrdersApiController> Logger)
        {
            _OrderService = OrderService;
            _Logger = Logger;
        }

        [HttpGet("user/{UserName}")]
        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName)
        {
            return await _OrderService.GetUserOrders(UserName);
        }

        [HttpGet("{id}")]
        public async Task<OrderDTO> GetOrderById(int id)
        {
            return await _OrderService.GetOrderById(id);
        }

        [HttpPost("{UserName}")]
        public async Task<OrderDTO> CreateOrder(string UserName, [FromBody] CreateOrderModel OrderModel)
        {
            _Logger.LogInformation("Формирование заказа для пользователя {0}", UserName);
            var order = await _OrderService.CreateOrder(UserName, OrderModel);

            return order;
        }
    }
}
