using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Assignment1_Server.Models;

namespace Assignment1_Server.Persistence
{
    public class FileContext
    {
        public IList<User> Users { get; private set; }
        public IList<Adult> Adults { get; private set; }

        private readonly string usersFile = "Persistence/Users/users.json";
        private readonly string adultsFile = "Persistence/Adults/adults.json";

        public FileContext()
        {
            Users = File.Exists(usersFile) ? ReadData<User>(usersFile) : new List<User>();
            Adults = File.Exists(adultsFile) ? ReadData<Adult>(adultsFile) : new List<Adult>();
        }

        private IList<T> ReadData<T>(string s)
        {
            using (var jsonReader = File.OpenText(s))
            {
                return JsonSerializer.Deserialize<List<T>>(jsonReader.ReadToEnd());
            }
        }

        public void SaveChanges()
        {
            // storing users
            string jsonUsers = JsonSerializer.Serialize(Users, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            using (StreamWriter outputFile = new StreamWriter(usersFile, false))
            {
                outputFile.Write(jsonUsers);
            }

            // storing persons
            string jsonAdults = JsonSerializer.Serialize(Adults, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            using (StreamWriter outputFile = new StreamWriter(adultsFile, false))
            {
                outputFile.Write(jsonAdults);
            }
        }
    }
}