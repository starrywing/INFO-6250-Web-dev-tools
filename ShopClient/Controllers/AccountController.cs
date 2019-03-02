using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopClient.Models;
using ShopClient.Models.Clients;
using ShopClient.Models.ViewModels;

namespace ShopClient.Controllers
{   [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private UserClient _userClient;

        public AccountController(UserClient userclient)
        {
            _userClient = userclient;
        }


        // login method for the user login 
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            LoginEntity loginInfo = new LoginEntity(model.Email, model.Password);
            User user = await _userClient.LoginAsync(loginInfo);
            if (null == user) {
                return View("LoginError");
            }
            return RedirectToAction("Index", "Home");
        }

        // register method for the user to register 
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            User user = new User(model.Username, model.Email, model.Password);
            string name = (await _userClient.RegisterAsync(user)).Username;
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View("Login");
        }


        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Logout()
        {
            _userClient.Logout();
            return RedirectToAction("Index", "Home");
        }

    }
}