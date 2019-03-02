using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopServer.Models;
using ShopServer.Models.Managers;

namespace ShopServer.Controllers
{
 
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {

        private readonly UserManager _userManager;


        public AccountController(UserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userManager.GetAllUsers();
        }

        [HttpPost]
        [Route("Login")]
        [ActionName("Login")]
        public User Login([FromBody]LoginEntity loginEntity)
        {
            string logEmailAddress = loginEntity.Email;
            string logPwd = loginEntity.Password;
            User loginUser = _userManager.Login(logEmailAddress, logPwd);
            return loginUser;
        }


        [HttpPost]
        [Route("[action]")]
        [ActionName("Register")]
        public User Register([FromBody]User user)
        {
            User registerUser = _userManager.RegisterUser(user);
            return registerUser;
        }


        [HttpPost]
        [Route("[action]")]
        [ActionName("Update")]
        public User Update([FromBody]User user)
        {
            User loginUser = _userManager.Update(user);
            return loginUser;
        }
    }
}