using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopServer.Models.Managers
{
    public class ProductManager
    {
        private readonly IMongoCollection<Product> _products;

        public ProductManager(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("shopdb"));
            var database = client.GetDatabase("shopdb");
            _products = database.GetCollection<Product>("Products");
        }

        public List<Product> GetAllProducts()
        {
            List<Product> list = _products.Find(products => true).ToList();
            if (null==list ||list.Capacity==0) {
                list = new List<Product>();
            };
            return list;
        }
        
        public Product GetProductById(string id)
        {
            return _products.Find(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public void CreateProduct(Product product)
        {
            _products.InsertOne(product);
        }

        public bool DeleteProduct(string id)
        {
            var deleteResult = _products.DeleteOne(x => x.Id.Equals(id));
            return deleteResult.DeletedCount != 0;
        }

        public bool UpdateProduct(Product p)
        {
            var replaceOneResult = _products.ReplaceOne(product => product.Id.Equals(p.Id), p);
            return replaceOneResult.ModifiedCount != 0;
        }

        public bool UpdateProductPrice(double newPrice, Product product)
        {
            product.Price = newPrice;
            var filter = Builders<Product>.Filter.Eq(x => x.Id, product.Id);
            var replaceOneResult = _products.ReplaceOne(doc => doc.Id.Equals(product.Id), product);
            return replaceOneResult.ModifiedCount != 0;
        }


    }
}
