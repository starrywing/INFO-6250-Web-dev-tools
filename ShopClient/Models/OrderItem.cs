using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopClient.Models
{
    public class OrderItem
    {
        public string ProductId;
        public string ProductName;
        public double ProductPrice;
        public string ProductImageUri;
        public int Quantity;
        public double Total;

        public OrderItem(string productId,string productName,double price,string productImageUri)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.ProductPrice = price;
            this.ProductImageUri = productImageUri;
            this.Quantity = 0;
            this.Total = 0;
        }

        public void AddOne()
        {
            Quantity = Quantity + 1;
            Total = Total + ProductPrice;
        }

        public void Remove(int quantity)
        {
            Quantity = Quantity - quantity;
            Total = Total - ProductPrice * quantity;
        }

        public void RemoveAll()
        {
            Quantity = 0;
            Total = 0;
        }

        public void Update(int quantity)
        {
            Quantity = quantity;

            Total =ProductPrice * quantity;
        }

        public int GetQuantity()
        {
            return Quantity;
        }

        public double GetTotal()
        {
            return Total;
        }
    }
}
