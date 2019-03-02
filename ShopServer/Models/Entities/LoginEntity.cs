using MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopServer.Models
{
    public class LoginEntity
    {
        [Required]
        [Display(Name = "Email")]
        public string Email;
        [Required]
        [Display(Name = "Password")]
        public string Password;

        public LoginEntity(String Email, string Password)
        {
            this.Email = Email;
            this.Password = Password;
        }
    }
}
