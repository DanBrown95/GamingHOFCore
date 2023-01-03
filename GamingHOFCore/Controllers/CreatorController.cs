using GamingHOFCore.DataAccess.Interfaces;
using GamingHOFCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamingHOFCore.Controllers
{
    //[Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CreatorController : ControllerBase
    {
        private readonly ILogger<CreatorController> _logger;
        private readonly ICreatorRepository _creatorRepo;

        public CreatorController(ILogger<CreatorController> logger, ICreatorRepository creatorRepo)
        {
            _logger = logger;
            _creatorRepo = creatorRepo;
        }

        [HttpPost]
        public async Task<Creator> GetCreatorDetails([FromBody] string id)
        {
            var creator = await _creatorRepo.GetDetails(id);
            return creator;
        }
    }
}
