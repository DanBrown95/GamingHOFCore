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
                var query = "SELECT * FROM Submission WHERE IsProcessed = 1 AND IsApproved = 1";
                var submissions = await connection.QueryAsync<Submission>(query);
                return submissions.ToList();
            }
        }

        public async Task<Submission> GetByIdAsync(string id)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"select s.*, u.Id, u.Firstname, u.Lastname 
                                from Submission s
                                INNER JOIN [User] u
                                ON s.UserId = u.Id
                                WHERE s.Id = '" + id + "'";
                var submissions = await connection.QueryAsync<Submission, User, Submission>(query, (submission, user) =>
                {
                    submission.Creator = user;
                    return submission;
                },
                splitOn: "Id");

                return submissions.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Submission>> GetAllIncludingCreatorAsync()
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"select s.*, u.Id, u.Firstname, u.Lastname 
                                from Submission s
                                INNER JOIN [User] u
                                ON s.UserId = u.Id
                                WHERE s.IsProcessed = 1 AND s.IsApproved = 1";

                var submissions = await connection.QueryAsync<Submission, User, Submission>(query, (submission, user) =>
                {
                    submission.Creator = user;
                    return submission;
                },
                splitOn: "Id");
                return submissions.ToList();
            }
        }

        public async Task<IEnumerable<Submission>> GetAllByPlatformAsync(string platform)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = string.Format("SELECT * FROM Submission WHERE Platform = '{0}' AND IsProcessed = 1 AND IsApproved = 1", platform);
                var submissions = await connection.QueryAsync<Submission>(query);
                return submissions.ToList();
            }
        }
    }
}
