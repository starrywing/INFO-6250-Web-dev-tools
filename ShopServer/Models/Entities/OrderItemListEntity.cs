using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopServer.Models.Entities
{
    public class OrderItemListEntity
    {
        public List<OrderItem> orderItems;

        public OrderItemListEntity()
        {
            orderItems = new List<OrderItem>();
        }
    }
}
