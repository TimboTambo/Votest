using System;
using System.Linq;
using Voting.Services.Models;

namespace Voting.Services
{
    public interface IPollService
    {
        int SavePoll(CreatePollModel poll, Guid userId);
        void SaveResponse(CompletePollModel response);
        Poll GetPoll(string searchTerm);
        Poll GetPoll(int pollId);
        Poll StartPoll(int pollId);
        IQueryable<Poll> GetAllUsersPolls(Guid userId);
    }
}
