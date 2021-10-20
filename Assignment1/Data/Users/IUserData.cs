using Assignment1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment1.Data.Users
{
    public interface IUserData
    {
        User Get(string username, string password);
        Task Add(User user);
        Task<IList<User>> GetUsers();
    }
}
