using GamingHOFCore.Models;
using GamingHOFCore.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamingHOFCore.DataAccess.Interfaces
{
    public interface ISubmissionRepository
    {
        Task<IEnumerable<Submission>> GetAllAsync();
        Task<IEnumerable<Submission>> GetAllByPlatformAsync(int platformId);
        Task<IEnumerable<SubmissionVM>> GetAllByCreatorAsync(string id);
        Task<IEnumerable<SubmissionVM>> GetAllIncludingCreatorAsync();
        Task<SubmissionVM> GetByIdAsync(string id);
        Task<bool> VoteSubmission(string userId, string submissionId);
    }
}
