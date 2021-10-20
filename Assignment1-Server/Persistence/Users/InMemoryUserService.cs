using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Assignment1_Server.Models;
using Assignment1_Server.Persistence;

namespace Assignment1_Server.Persistence
{
    public class InMemoryUserService : IUserService
    {
        public IList<User> Users { get; private set; }
        private readonly FileContext fileContext;

        public InMemoryUserService()
        {
            fileContext = new FileContext();
            Users = fileContext.Users;
        }

        public async Task<IList<User>> GetUsers()
        {
            return Users;
        }

        public User ValidateUser(string username, string password)
        {
            User first = Users.FirstOrDefault(x => x.Username.Equals(username));
            /*if (first == null) throw new Exception("User not found");
            if (!first.Password.Equals(password)) throw new Exception("Invalid password");*/
            if (first != null) throw new Exception("User already exists");
            return first;
        }

        public void SaveChanges()
        {
            fileContext.SaveChanges();
        }

        public async Task<User> Add(User user)
        {
            await Task.Run(() =>
            {
                user.IsRegistered = true;
                Users.Add(user);
                SaveChanges();
            });

            return user;
        }
    }
}
