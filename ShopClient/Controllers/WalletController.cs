using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopClient.Models;
using ShopClient.Models.Clients;
using ShopClient.Models.ViewModels;

namespace ShopClient.Controllers
{
    public class WalletController : Controller
    {
        private UserClient _userClient;
        List<PaymentMethod> _paymentMethods;

        public WalletController(UserClient userClient) {
             _userClient = userClient;
        }

        public IActionResult Index()
        {
            _paymentMethods = _userClient.GetCurrentUser().GetPaymentMethods();
            return View(_paymentMethods);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPaymentMethodAsync(PaymentMethodViewModel model)
        {
            PaymentMethod p = new PaymentMethod(model.CardNum);
            _userClient.GetCurrentUser().addPaymentMethod(p);
            await _userClient.UpdateAsync(_userClient.GetCurrentUser());
            return RedirectToAction("Index", "Wallet");
        }
    }
}