using GamingHOFCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamingHOFCore.DataAccess.Interfaces
{
    public interface ISubmissionRepository
    {
        Task<IEnumerable<Submission>> GetAllAsync();
        Task<IEnumerable<Submission>> GetAllByPlatformAsync(string platform);
        Task<IEnumerable<Submission>> GetAllIncludingCreatorAsync();
        Task<Submission> GetByIdAsync(string id);
    }
}
