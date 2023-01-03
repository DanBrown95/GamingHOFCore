using Dapper;
using GamingHOFCore.DataAccess.Interfaces;
using GamingHOFCore.Models;
using GamingHOFCore.Models.ViewModels;
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
                var query = @"SELECT Submission.Id, Submission.Name, Submission.PlatformId, Submission.GameId, Submission.Votes, 
                                     Submission.Rank, Submission.Image, Submission.Submitted, Game.Name as GameName 
                              FROM Submission
                              INNER JOIN [Game]
                              ON Submission.GameId = Game.Id
                              WHERE IsProcessed = 1 AND IsApproved = 1";
                return await connection.QueryAsync<Submission>(query);
            }
        }

        public async Task<SubmissionVM> GetByIdAsync(string id)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"select Submission.*, [User].Id as userId, [User].Gamertag, Game.Name as GameName, Platform.Name as PlatformName
                                from Submission
                                INNER JOIN [User]
                                ON Submission.UserId = [User].Id
                                INNER JOIN [Platform]
                                ON Submission.PlatformId = Platform.Id
                                INNER JOIN [Game]
                                ON Submission.GameId = Game.Id
                                WHERE Submission.Id = '" + id + "'";
                return await connection.QuerySingleOrDefaultAsync<SubmissionVM>(query);
            }
        }

        public async Task<IEnumerable<SubmissionVM>> GetAllByCreatorAsync(string id)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"select s.Id, s.Name, s.PlatformId, s.GameId, s.Votes, s.[Url], s.[Rank], s.[Image], s.Submitted, creator.Id as creatorId, creator.Gamertag, Game.Name as GameName, Platform.Name as PlatformName
                                from Submission s
                                INNER JOIN [User] creator
                                ON s.UserId = creator.Id
                                INNER JOIN [Game]
                                ON s.GameId = Game.Id
                                INNER JOIN [Platform]
                                ON s.PlatformId = Platform.Id
                                WHERE s.UserId = '" + id + "'";
                return await connection.QueryAsync<SubmissionVM>(query);
            }
        }

        public async Task<IEnumerable<SubmissionVM>> GetAllIncludingCreatorAsync()
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"select s.Id, s.Name, s.PlatformId, s.GameId, s.Votes, s.[Url], s.[Rank], s.[Image], s.Submitted, creator.Id as creatorId, creator.Gamertag, Game.Name as GameName, Platform.Name as PlatformName
                                from Submission s
                                INNER JOIN [User] creator
                                ON s.UserId = creator.Id
                                INNER JOIN [Game]
                                ON s.GameId = Game.Id
                                INNER JOIN [Platform]
                                ON s.PlatformId = Platform.Id
                                WHERE s.IsProcessed = 1 AND s.IsApproved = 1";

                return await connection.QueryAsync<SubmissionVM>(query);
            }
        }

        public async Task<IEnumerable<Submission>> GetAllByPlatformAsync(int platformId)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = string.Format("SELECT * FROM Submission WHERE Platform = '{0}' AND IsProcessed = 1 AND IsApproved = 1", platformId);
                var submissions = await connection.QueryAsync<Submission>(query);
                return submissions.ToList();
            }
        }

        public async Task<bool> VoteSubmission(string userId, string submissionId)
        {
            using (var connection = _context.CreateConnection())
            {

                var query = @"
                    IF NOT EXISTS (SELECT uv.* 
				        FROM UserVotes uv
				        INNER JOIN Submission s
				        ON uv.SubmissionId = s.Id
				        WHERE uv.UserId = '"+userId+"' AND uv.SubmissionId = '"+submissionId+@"')
                            BEGIN
                               IF NOT EXISTS ( SELECT * FROM Submission WHERE Id = '"+submissionId+"' AND UserId = '"+userId+@"' )
						        BEGIN 
							        SET XACT_ABORT ON

							        begin transaction

							        INSERT INTO UserVotes ([UserId],[SubmissionId],[VoteDate]) VALUES ('"+userId+"', '"+submissionId+@"',GETDATE())
							        UPDATE Submission SET Votes = Votes + 1 WHERE Id = '"+submissionId+@"'

							        commit transaction
						        END 
                            END
                ";

                return await connection.ExecuteAsync(query) > 0;
            }
        }

    }
}
