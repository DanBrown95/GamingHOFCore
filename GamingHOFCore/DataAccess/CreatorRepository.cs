using Dapper;
using GamingHOFCore.DataAccess.Interfaces;
using GamingHOFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamingHOFCore.DataAccess
{
    public class CreatorRepository : ICreatorRepository
    {
        private readonly ConnectionFactory _context;

        public CreatorRepository(ConnectionFactory context)
        {
            _context = context;
        }

        public async Task<Creator> GetDetails(string id)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "SELECT * FROM [dbo].[User] where Id = " + id;
                var creator = await connection.QueryAsync<Creator>(query);
                return creator.FirstOrDefault();
            }
        }
    }
}
