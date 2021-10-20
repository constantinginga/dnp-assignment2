using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1_Server.Models;

namespace Assignment1_Server.Persistence
{
    public interface IUserService
    {
        Task<IList<User>> GetUsers();

        User ValidateUser(string username, string password);
        void SaveChanges();
        Task<User> Add(User user);
    }
}
