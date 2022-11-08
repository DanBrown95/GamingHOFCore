using Dapper;
using GamingHOFCore.DataAccess.Interfaces;
using GamingHOFCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GamingHOFCore.DataAccess
{
    public class SubmissionRepository : ISubmissionRepository
    {
        private readonly ConnectionFactory _context;

        public SubmissionRepository(ConnectionFactory context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Submission>> GetAllAsync()
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "SELECT * FROM Submission";
                var submissions = await connection.QueryAsync<Submission>(query);
                return submissions.ToList();
            }
        }

        public async Task<IEnumerable<Submission>> GetAllByPlatformAsync(string platform)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = string.Format("SELECT * FROM Submission WHERE Platform = '{0}'", platform);
                var submissions = await connection.QueryAsync<Submission>(query);
                return submissions.ToList();
            }
        }
    }
}
