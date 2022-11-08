using GamingHOFCore.DataAccess.Interfaces;
using GamingHOFCore.Models;
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

        public async Task<IEnumerable<Submission>> GetAllByPlatformAsync(string platform)
        {
            var submissions = await _submissionRepo.GetAllByPlatformAsync(platform);
            return submissions;
        }
    }
}
