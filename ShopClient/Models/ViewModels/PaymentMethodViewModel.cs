using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopClient.Models.ViewModels
{
    public class PaymentMethodViewModel

    {   [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} numbers long.", MinimumLength = 6)]
        [Display(Name = "CardNum")]
        public string CardNum { get; set; }


    }
}
