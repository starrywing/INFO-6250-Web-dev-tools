using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopClient.Models;
using ShopClient.Models.Clients;

namespace ShopClient.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController : Controller
    {
        private UserClient _userClient;
        private ProductClient _productClient;
        private CartEntity _cart;

        public CartController(UserClient userClient, ProductClient productClient) {
            _userClient = userClient;
            _productClient = productClient;
            _cart = userClient.GetCurrentUser().GetCart();
        }

        public IActionResult Index()
        {
           // System.Diagnostics.Debug.WriteLine("id ===" + id);
            return View();
        }

        public async Task<IActionResult> AddAsync(string id)
        {
            Product p = _productClient.GetProductById(id);
            _cart.Add(p.Id,p.Name, p.Price,p.Photo);
            if (_userClient.IsSignedIn()) {
                await _userClient.UpdateAsync(_userClient.GetCurrentUser());
                return RedirectToAction("Index", "Cart");
            }
            else {
                return RedirectToAction("Login", "Account");
            }
            
        }

        [Route("RemoveAsync/{id}")]
        public async Task<IActionResult> RemoveAsync(string id)
        {

            System.Diagnostics.Debug.WriteLine(id);
            _cart.Remove(id);
            await _userClient.UpdateAsync(_userClient.GetCurrentUser());
            return RedirectToAction("Index", "Cart");
        }


        public IActionResult SelectPaymentMethod()
        {
            return View();
        }
    }
}