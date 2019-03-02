using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ShopClient.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopClient.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id;

        [BsonElement("UserId")]
        public string UserId;

        public PaymentMethod paymentMethod;

        public OrderItemListEntity orderItems;

        public Order(string userId, PaymentMethod paymentMethod)
        {
            orderItems = new OrderItemListEntity();
            this.paymentMethod = paymentMethod;
            this.UserId = userId;
        }

        public void AddOrderItems(List<OrderItem> ois) {
            foreach (OrderItem oi in ois)
            {
                orderItems.orderItems.Add(oi);
            }
        }

        public int CountOrderItems()
        {
            int result = 0;
            foreach (OrderItem oi in orderItems.orderItems)
            {
                result += oi.GetQuantity();
            }
            return result;
        }

        public double GetTotalAmount()
        {
            double result = 0;
            foreach (OrderItem oi in orderItems.orderItems)
            {
                result += oi.GetTotal();
            }
            return result;
        }
    }

}
