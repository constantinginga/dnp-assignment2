using Assignment1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.Data.Users
{
    public class UserData : IUserData
    {

        private HttpClient client;
        private const string _url = "https://localhost:5002/api/Users";
        public IList<User> Users {  get; set; }
        
        public UserData()
        {
            client = new();
            Users = new List<User>();
        }

        public async Task<IList<User>> GetUsers()
        {
            // load once and store in variable
            if (!Users.Any())
            {
                HttpResponseMessage response = await client.GetAsync(_url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                
                Users = JsonConvert.DeserializeObject<List<User>>(responseBody);
                /*// have to do this because of the inheritance
                JsonSerializerSettings settings = new() { TypeNameHandling = TypeNameHandling.All };
                Adults = JsonConvert.DeserializeObject<List<Adult>>(responseBody, settings);*/
            }

            return Users;
        }

        public async Task<User> Get(string username, string password)
        {
            if (Users.Count == 0) await GetUsers();
            User first = Users.FirstOrDefault(x => x.Username.Equals(username));
            if (first == null) throw new Exception("User not found");
            if (!first.Password.Equals(password)) throw new Exception("Invalid password");
            return first;
        }

        public async Task Add(User user)
        {
            User first = Users.FirstOrDefault(u => u.Username.Equals(user.Username));
            if (first != null) throw new Exception("User already exists");

            StringContent queryString = new(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(_url, queryString);
            response.EnsureSuccessStatusCode();
            Users.Add(JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync()));
        }
    }
}
