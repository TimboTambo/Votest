using System;
using System.Collections.Generic;
using System.Linq;
using Voting.Services.Models;

namespace Voting.Services.Implementation.Repositories
{
    public interface IPollRepository
    {
        int SavePoll(Poll poll);
        Poll GetPoll(string searchText);
        void SaveResponse(Response response);
        Poll GetPoll(int pollId);
        Poll StartPoll(int pollId);
        void UpdateVotes(int pollId, List<QuestionResponse> questionResponses);
        IQueryable<Poll> GetAllUserPolls(Guid userId);
    }
}
