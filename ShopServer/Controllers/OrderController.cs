using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopServer.Models;
using ShopServer.Models.Managers;

namespace ShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {

        private readonly OrderManager _orderManager;

        public OrderController(OrderManager orderManager) {
            _orderManager = orderManager;
        }

       // [HttpGet("{id}")]
        [HttpPost("{userId}")]
        [Route("[action]")]
        [ActionName("GetOrderById")]
        public IEnumerable<Order> GetOrderById([FromBody]string userId)
        {
            return _orderManager.GetOrderByUserId(userId);
        }

        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _orderManager.GetOrders();
        }

        [HttpPost]
        [Route("[action]")]
        [ActionName("CreateOrder")]
        public Order CreateOrder([FromBody]Order order)
        {
            Order newOrder = _orderManager.CreateOrder(order);
            return newOrder;
        }


    }
}