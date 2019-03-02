using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopServer.Models
{
    public class RegisterEntity
    {
       
        public string Id;
        public string Username;
        public string Email;
        public string Password;
    }
}
