using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShopClient.Models.Clients
{
    public class UserClient
    {
        private Uri _hostUri;
        private User CurrentUser;
        private User DefaultUser;

        public UserClient(IConfiguration config)
        {
            var configString = config.GetSection("ClientConnectionStrings")["Server"];
            DefaultUser = new User("Guest", "Guest", "Guest");

            //default user
            if(CurrentUser == null) {
                CurrentUser = DefaultUser;
            }

            _hostUri = new Uri(configString);
        }

        public async Task<User> LoginAsync(LoginEntity loginInfo)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_hostUri, "api/Account/Login");
            using (client)
            {
                HttpResponseMessage response;
                var output = JsonConvert.SerializeObject(loginInfo);
                HttpContent contentPost = new StringContent(output, System.Text.Encoding.UTF8, "application/json");
                response = await client.PostAsync(client.BaseAddress, contentPost);
                var avail = await response.Content.ReadAsStringAsync()
                        .ContinueWith<User>(postTask =>
                        {
                            return JsonConvert.DeserializeObject<User>(postTask.Result);
                        });
                CurrentUser = avail;
                return avail;
            }
        }


        public async Task<User> RegisterAsync(User user)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_hostUri, "api/Account/Register");
            using (client)
            {
                HttpResponseMessage response;
                var output = JsonConvert.SerializeObject(user);
                HttpContent contentPost = new StringContent(output, System.Text.Encoding.UTF8, "application/json");
                response = await client.PostAsync(client.BaseAddress, contentPost);
                var avail = await response.Content.ReadAsStringAsync()
                        .ContinueWith<User>(postTask =>
                        {
                            return JsonConvert.DeserializeObject<User>(postTask.Result);
                        });
                CurrentUser = avail;
                return avail;
            }
        }

        public async Task<HttpStatusCode> UpdateAsync(User user)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_hostUri, "api/Account/Update");
            using (client)
            {
                HttpResponseMessage response;
                var output = JsonConvert.SerializeObject(user);
                HttpContent contentPost = new StringContent(output, System.Text.Encoding.UTF8, "application/json");
                response = await client.PostAsync(client.BaseAddress, contentPost);
                return response.StatusCode;
            }
        }

        public User GetCurrentUser() {
            if (null == CurrentUser) {
                CurrentUser = DefaultUser;
            }
            return CurrentUser;
        }


        public bool IsSignedIn() {
           if (null == CurrentUser) {
                return false;
            }
            if (CurrentUser.Username.Equals(DefaultUser.Username)) {
                return false;
            }
            return true;
        }


        public void Logout() {
           CurrentUser = DefaultUser;
        }
    }
}
