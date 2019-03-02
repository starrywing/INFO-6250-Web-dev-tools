using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopClient.Models.Clients
{
    public class ProductClient
    {
        HttpClient client;
        string _hostUri;

        public ProductClient(IConfiguration config)
        {
            var configString = config.GetSection("ClientConnectionStrings")["Server"];
            System.Diagnostics.Debug.WriteLine("sidhsidhasuihafs   == " + configString);
            _hostUri = configString;
        }

        public HttpClient CreateClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(new Uri(_hostUri), "api/Product/");
            return client;
        }

        public HttpClient CreateActionClient(string action)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(new Uri(_hostUri), "api/Product/" + action);
            return client;
        }

        public async Task<List<Product>> GetProductAsync()
        {
            using (var client = CreateClient())
            {
                HttpResponseMessage response;
                response = client.GetAsync(client.BaseAddress).Result;
                if (response.IsSuccessStatusCode)
                {
                    var avail = await response.Content.ReadAsStringAsync()
                        .ContinueWith<List<Product>>(postTask =>
                        {
                            return JsonConvert.DeserializeObject<List<Product>>(postTask.Result);
                        });
                    return avail;
                }
                else
                {
                    return new List<Product>();
                }
            }
        }


        public System.Net.HttpStatusCode CreateProduct(Product product)
        {
            using (var client = CreateActionClient("CreateProduct"))
            {
                HttpResponseMessage response = null;
                try
                {
                    var output = JsonConvert.SerializeObject(product);
                    HttpContent contentPost = new StringContent(output, System.Text.Encoding.UTF8, "application/json");
                    response = client.PostAsync(client.BaseAddress, contentPost).Result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return response.StatusCode;
            }
        }

        public Product GetProductById(string id)
        {
            using (var client = CreateClient())
            {
                HttpResponseMessage response;
                response = client.GetAsync(new Uri(client.BaseAddress, id)).Result;

                if (response.IsSuccessStatusCode)
                {
                    var avail =  response.Content.ReadAsStringAsync()
                        .ContinueWith<Product>(postTask =>
                        {
                            return JsonConvert.DeserializeObject<Product>(postTask.Result);
                        });
                    return avail.Result;
                }
                else
                {
                    return null;
                }
            }
        }

        public System.Net.HttpStatusCode DeleteProduct(String id)
        {
            using (var client = CreateClient())
            {
                HttpResponseMessage response;
                response = client.DeleteAsync(new Uri(client.BaseAddress, id.ToString())).Result;
                response = client.DeleteAsync(new Uri(client.BaseAddress, id.ToString())).Result;
                return response.StatusCode;
            }
        }
    }
}
