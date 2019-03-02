using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopServer.Models.Managers
{
    public class OrderManager
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderManager(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("shopdb"));
            var database = client.GetDatabase("shopdb");
            _orders = database.GetCollection<Order>("Orders");
        }

        public IEnumerable<Order> GetOrderByUserId(string userId)
        {
            return _orders.Find(x => x.UserId.Equals(userId)).ToList();
        }

        public IEnumerable<Order> GetOrders()
        {
            return _orders.Find(x=>true).ToList();
        }

        public Order CreateOrder(Order order)
        {
            _orders.InsertOne(order);
            return order;
        }
    }

    
}
