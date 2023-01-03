using GamingHOFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamingHOFCore.DataAccess.Interfaces
{
    public interface ICreatorRepository
    {
        Task<Creator> GetDetails(string id);
    }
}
