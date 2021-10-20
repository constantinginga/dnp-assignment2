using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1_Server.Models;

namespace Assignment1_Server.Persistence
{
    public interface IAdultData
    {
        Task<Adult> Add(Adult adult);
        Task<IList<Adult>> GetAdults();

        Task RemoveAdult(Adult adult);
        Task UpdateAdult(Adult adult);

        Adult Get(int id);
    }
}
