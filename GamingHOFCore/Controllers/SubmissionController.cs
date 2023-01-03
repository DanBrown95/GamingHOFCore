using GamingHOFCore.DataAccess.Interfaces;
using GamingHOFCore.Models;
using GamingHOFCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamingHOFCore.Controllers
{
    //[Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly ILogger<SubmissionController> _logger;
        private readonly ISubmissionRepository _submissionRepo;

        public SubmissionController(ILogger<SubmissionController> logger, ISubmissionRepository submissionRepo)
        {
            _logger = logger;
            _submissionRepo = submissionRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<Submission>> GetAllAsync()
        {
            var submissions = await _submissionRepo.GetAllAsync();
            return submissions;
        }

        [HttpPost]
        public async Task<Submission> GetById([FromBody] string id)
        {
            var x = await _submissionRepo.GetByIdAsync(id);
            var submission = new Submission()
            {
                Id = x.Id,
                Name = x.Name,
                Rank = x.Rank,
                Votes = x.Votes,
                PlatformId = x.PlatformId,
                Url = x.Url,
                Image = x.Image,
                Submitted = x.Submitted,
                Creator = new Models.User() { Gamertag = x.Gamertag, Id = x.UserId },
                Game = new Game() { PlatformId = x.PlatformId, Name = x.GameName },
                Platform = new Platform() { Id = x.PlatformId, Name = x.PlatformName}
            };

            return submission;
        }

        [HttpGet]
        public async Task<IEnumerable<Submission>> GetAllIncludingCreatorAsync()
        {
            var apiData = await _submissionRepo.GetAllIncludingCreatorAsync();
            
            List<Submission> final = new List<Submission>();
            foreach (var x in apiData)
            {
                var submission = new Submission()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Rank = x.Rank,
                    Votes = x.Votes,
                    PlatformId = x.PlatformId,
                    Url = x.Url,
                    Image = x.Image,
                    Submitted = x.Submitted,
                    Creator = new Models.User() { Gamertag = x.Gamertag, Id = x.UserId },
                    Game = new Game() { PlatformId = x.PlatformId, Name = x.GameName },
                    Platform = new Platform() { Id = x.PlatformId, Name = x.PlatformName }
                };
                final.Add(submission);
            }

            return final;
        }

        public async Task<IEnumerable<Submission>> GetAllByPlatformAsync(int platform)
        {
            var submissions = await _submissionRepo.GetAllByPlatformAsync(platform);
            return submissions;
        }

        [HttpPost]
        public async Task<IEnumerable<Submission>> GetAllByCreator([FromBody] string id)
        {
            var apiData = await _submissionRepo.GetAllByCreatorAsync(id);
            
            List<Submission> final = new List<Submission>();
            foreach (var x in apiData)
            {
                var submission = new Submission()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Rank = x.Rank,
                    Votes = x.Votes,
                    PlatformId = x.PlatformId,
                    Url = x.Url,
                    Image = x.Image,
                    Submitted = x.Submitted,
                    GameId = x.GameId,
                    Creator = new Models.User() { Gamertag = x.Gamertag, Id = x.UserId },
                    Game = new Game() { PlatformId = x.PlatformId, Name = x.GameName, Id = x.GameId },
                    Platform = new Platform() { Id = x.PlatformId, Name = x.PlatformName }
                };
                final.Add(submission);
            }
            
            return final;
        }

        [HttpPost]
        public async Task<bool> Upvote([Bind] UpvoteVM model)
        {
            return await _submissionRepo.VoteSubmission(model.UserId, model.SubmissionId);
        }
    }
}
