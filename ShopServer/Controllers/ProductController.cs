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
    public class ProductController : ControllerBase
    {
        private readonly ProductManager _productService;

        public ProductController(ProductManager productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            return _productService.GetAllProducts();
        }

        [HttpGet("{id}")]
        public async Task<Product> GetProductById(string id)
        {
            return await GetProductByIdAsync(id);
        }

        private Task<Product> GetProductByIdAsync(string id)
        {
            return Task.FromResult(_productService.GetProductById(id));
        }

        [HttpPost]
        [Route("[action]")]
        [ActionName("CreateProduct")]
        public StatusCodeResult CreateProduct(Product product)
        {
                _productService.CreateProduct(product);
                return new StatusCodeResult(201); //created
        }

        [HttpDelete("{id}")]
        public bool DeleteProduct(string id)
        {
            return _productService.DeleteProduct(id);
        }

        [HttpPost]
        [Route("[action]")]
        [ActionName("UpdateProductPrice")]
        public StatusCodeResult UpdateProductPrice(double price, Product product)
        {
            if (product == null)
            {
                return new BadRequestResult();
            }
            else
            {
                _productService.UpdateProductPrice(price, product);
                return new StatusCodeResult(200); //succeed
            }
        }

        [HttpPost]
        [Route("[action]")]
        [ActionName("Boom")]
        public StatusCodeResult Boom(Product product)
        {
                throw new Exception();
        }

    }
}