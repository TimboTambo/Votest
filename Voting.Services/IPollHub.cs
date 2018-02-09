using System.Collections.Generic;
using Voting.Models;
using Voting.Services.Models;

namespace Voting.Hubs
{
    public interface IPollHub
    {
        void LinkConnectionToPollUpdates(int pollId);
        void StartPoll(int pollPollId);
        void UpdateVotes(PollRealtimeUpdate pollUpdate);
    }
}