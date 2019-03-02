using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopServer.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id;

        [BsonElement("Username")]
        public string Username { private set; get; }

        [BsonElement("Email")]
        public string Email { private set; get; }

        [BsonElement("Password")]
        public string Password { private set; get; }

        public List<PaymentMethod> paymentMethods;

        public CartEntity CartEntity;

        public User(string username, string email, string password)
        {
            this.Username = username;
            this.Email = email;
            this.Password = password;
            paymentMethods = new List<PaymentMethod>();
            CartEntity = new CartEntity();
        }

        public void addPaymentMethod(PaymentMethod p)
        {
            paymentMethods.Add(p);
        }

        public List<PaymentMethod> GetPaymentMethods()
        {
            return paymentMethods;
        }

        public CartEntity GetCart()
        {
            return CartEntity;
        }
    }
}
