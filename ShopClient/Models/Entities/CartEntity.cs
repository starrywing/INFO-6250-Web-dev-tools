using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopClient.Models
{
    public class CartEntity
    {
        public List<OrderItem> orderItems;

        public CartEntity()
        {
            orderItems = new List<OrderItem>();
        }

        public void Add(string id,string name,double price,string image) {
            //add order item if no exsist
            if (null == orderItems.Find(o => o.ProductId.Equals(id)))
            {
                OrderItem oi = new OrderItem(id, name, price, image);
                orderItems.Add(oi);
                oi.AddOne();
            }
            else
            {
                //update orderitem quantity if exsist
                OrderItem oi = orderItems.Find(o => o.ProductId.Equals(id));
                oi.AddOne();
            }
           
        }

        public int CountOrderItems() {
            int result = 0;
            foreach (OrderItem oi in orderItems){
                result += oi.GetQuantity();
            }
            return result;
        }


        public double GetTotalAmount() {
            double result = 0;
            foreach (OrderItem oi in orderItems)
            {
                result += oi.GetTotal();
            }
            return result;
        }

        public List<OrderItem> GetOrderItems() {
            return orderItems;
        }

        public void Remove(string id) {
            OrderItem oi = orderItems.Find(o => o.ProductId.Equals(id));
            if (null != oi)
            {
                orderItems.Remove(oi);
            }
        }

        public void RemoveAll() {
            orderItems.RemoveAll(oi => true);
        }

    }
}
