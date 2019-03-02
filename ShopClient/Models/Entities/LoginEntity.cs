using MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopClient.Models
{
    public class LoginEntity
    {
            public string Email;

            public string Password;

            public LoginEntity(String Email, string Password)
            {
                this.Email = Email;
                this.Password = Password;
            }
    }
}
