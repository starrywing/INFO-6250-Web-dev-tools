using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ShopServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopServer.Models.Managers
{
    public class UserManager
    {
        private readonly IMongoCollection<User> _users;

        public UserManager(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("shopdb"));
            var database = client.GetDatabase("shopdb");
            _users = database.GetCollection<User>("Users");
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users.Find(products => true).ToList();
        }

        public User GetUserByEmail(string email)
        {
            return _users.Find<User>(u => u.Email.Equals(email)).FirstOrDefault();
        }


        public User GetUserById(string id)
        {
            return _users.Find<User>(u => u.Id.Equals(id)).FirstOrDefault();
        }


        public void CreateUser(User user)
        {
            _users.InsertOne(user);
        }

        public bool DeleteUser(string id)
        {
            var deleteResult = _users.DeleteOne(x => x.Id.Equals(id));
            return deleteResult.DeletedCount != 0;
        }

        // RegisterUser method 
        public User RegisterUser(User user)
        {
            User tempuser = _users.Find<User>(o => (o.Email.Equals(user.Email))).FirstOrDefault();
            if (null != tempuser)
            {   
                //user already exist in the database;
                return null;
            }
            else
            {
                CreateUser(user);
                return user;
            }
        }


        // login method 
        public User Login(string emailAddress, string password)
        {
            System.Diagnostics.Debug.WriteLine(emailAddress+":"+ password);
            User signedInUser = _users.Find<User>(o => (o.Email.Equals(emailAddress) && o.Password.Equals(password))).FirstOrDefault();
            if (null != signedInUser)
            {
                return signedInUser;
            }
            else {
                //user is not exist in the database;
                System.Diagnostics.Debug.WriteLine(" User can not find ");
                return signedInUser;
            }
        }


        public User Update(User user) {
            var result = _users.ReplaceOne(u => u.Id.Equals(user.Id), user);
            if (result.IsAcknowledged)
            {
                return user;
            }
            else {
                return null;
            }
        }
    }
}
