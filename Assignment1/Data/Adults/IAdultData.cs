using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1.Models;

namespace Assignment1.Data.Adults
{
    interface IAdultData
    {
        Task Add(Adult adult);
        Task<IList<Adult>> GetAdults();

        Task RemoveAdult(Adult adult);
        Task UpdateAdult(Adult adult);

        Adult Get(int id);
    }
}
