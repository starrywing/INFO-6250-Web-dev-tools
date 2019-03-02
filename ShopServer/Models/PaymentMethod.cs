using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopServer.Models
{
    public class PaymentMethod
    {
        public string CardNum { get; set; }

        public PaymentMethod(string num)
        {
            CardNum = num;
        }
    }
}
