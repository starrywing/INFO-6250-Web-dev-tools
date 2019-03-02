using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopClient.Models;
using ShopClient.Models.Clients;

namespace ShopClient.Controllers
{
    public class HomeController : Controller
    {
        private UserClient _userClient;
        private ProductClient _productClient;
        public HomeController(UserClient userclient,ProductClient productclient)
        {
            _userClient = userclient;
			_productClient = productclient;
        }
		
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //the method we use to crash the web site
        public void Boom() {
            throw new NotImplementedException();
        }
    }
}
