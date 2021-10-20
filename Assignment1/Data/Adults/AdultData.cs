using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Assignment1.Models;
using Newtonsoft.Json;

namespace Assignment1.Data.Adults
{
    public class AdultData : IAdultData
    {
        private HttpClient client;
        public IList<Adult> Adults { get; private set; }
        private const string _url = "https://localhost:5002/api/Adults";

        public AdultData()
        {
            client = new();
            Adults = new List<Adult>();
        }

        public async Task Add(Adult adult)
        {
            // store locally
            int max = Adults.Max(adult => adult.Id);
            adult.Id = ++max;
            Adults.Add(adult);

            // store in web api
            StringContent queryString = new(JsonConvert.SerializeObject(adult), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(_url, queryString);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IList<Adult>> GetAdults()
        {
            // load once and store in variable
            if (!Adults.Any())
            {
                HttpResponseMessage response = await client.GetAsync(_url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                // have to do this because of the inheritance
                JsonSerializerSettings settings = new() { TypeNameHandling = TypeNameHandling.All };
                Adults = JsonConvert.DeserializeObject<List<Adult>>(responseBody, settings);
            }

            return Adults;
        }

        public async Task RemoveAdult(Adult adult)
        {
            Adults.Remove(adult);
            HttpResponseMessage response = await client.DeleteAsync(_url + $"/{adult.Id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAdult(Adult adult)
        {
            Adult toUpdate = Adults.First(a => a.Id == adult.Id);
            toUpdate.FirstName = adult.FirstName;
            toUpdate.LastName = adult.LastName;
            toUpdate.HairColor = adult.HairColor;
            toUpdate.EyeColor = adult.EyeColor;
            toUpdate.Age = adult.Age;
            toUpdate.Height = adult.Height;
            toUpdate.Weight = adult.Weight;
            toUpdate.Sex = adult.Sex;
            toUpdate.JobTitle = adult.JobTitle;

            StringContent queryString = new(JsonConvert.SerializeObject(adult), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PatchAsync(_url + $"/{adult.Id}", queryString);
            response.EnsureSuccessStatusCode();
        }

        public Adult Get(int id)
        {
            return Adults.FirstOrDefault(a => a.Id == id);
        }
    }
}
