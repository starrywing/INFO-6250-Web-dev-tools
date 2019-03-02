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

    public class OrderClient
    {
        private Uri _hostUri;

        public OrderClient(IConfiguration config)
        {
            var configString = config.GetSection("ClientConnectionStrings")["Server"];
            _hostUri = new Uri(configString);
        }

        public async Task<IEnumerable<Order>> GetOrderByIdAsync(string userId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_hostUri, "api/Order/GetOrderById");
            using (client)
            {
                HttpResponseMessage response;
                var output = JsonConvert.SerializeObject(userId);
                HttpContent contentPost = new StringContent(output, System.Text.Encoding.UTF8, "application/json");
                response = await client.PostAsync(client.BaseAddress, contentPost);
                if (response.IsSuccessStatusCode)
                {
                    var avail = await response.Content.ReadAsStringAsync()
                        .ContinueWith<IEnumerable<Order>>(postTask =>
                        {
                            return JsonConvert.DeserializeObject<IEnumerable<Order>>(postTask.Result);
                        });
                    return avail;
                }
                else
                {
                    return new List<Order>();
                }
            }
        }

        public async Task<Order> CreateOrder(Order order)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(_hostUri, "api/Order/CreateOrder")
            };

            using (client)
            {
                HttpResponseMessage response;
                var output = JsonConvert.SerializeObject(order);
                System.Diagnostics.Debug.WriteLine(output);
                HttpContent contentPost = new StringContent(output, System.Text.Encoding.UTF8, "application/json");
                response = await client.PostAsync(client.BaseAddress, contentPost);
                var avail = await response.Content.ReadAsStringAsync()
                        .ContinueWith<Order>(postTask =>
                        {
                            return JsonConvert.DeserializeObject<Order>(postTask.Result);
                        });
                return avail;
            }
        }

    }
}
