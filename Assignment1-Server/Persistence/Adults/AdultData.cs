using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1_Server.Models;

namespace Assignment1_Server.Persistence
{
    public class AdultData : IAdultData
    {

        private FileContext fileContext;

        public AdultData()
        {
            fileContext = new FileContext();
        }

        public async Task<Adult> Add(Adult adult)
        {
            await Task.Run(() =>
            {
                int max = fileContext.Adults.Max(adult => adult.Id);
                adult.Id = ++max;
                fileContext.Adults.Add(adult);
                fileContext.SaveChanges();
            });

            return adult;
        }

        public async Task<IList<Adult>> GetAdults()
        {
            return fileContext.Adults;
        }

        public async Task RemoveAdult(Adult adult)
        {
            await Task.Run(() => fileContext.Adults.Remove(adult));
            fileContext.SaveChanges();
        }

        public async Task UpdateAdult(Adult adult)
        {
            await Task.Run(() =>
            {
                Adult toUpdate = fileContext.Adults.First(a => a.Id == adult.Id);
                toUpdate.FirstName = adult.FirstName;
                toUpdate.LastName = adult.LastName;
                toUpdate.HairColor = adult.HairColor;
                toUpdate.EyeColor = adult.EyeColor;
                toUpdate.Age = adult.Age;
                toUpdate.Height = adult.Height;
                toUpdate.Weight = adult.Weight;
                toUpdate.Sex = adult.Sex;
                toUpdate.JobTitle = adult.JobTitle;
                fileContext.SaveChanges();
            });
        }

        public Adult Get(int id)
        {
            return fileContext.Adults.FirstOrDefault(a => a.Id == id);
        }
    }
}
