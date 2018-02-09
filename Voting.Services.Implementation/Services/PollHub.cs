using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Voting.Hubs;
using Voting.Services.Models;

namespace Voting.Services.Implementation.Services
{
    public class PollHub : Hub, IPollHub
    {
        public void UpdateVotes(PollRealtimeUpdate pollUpdate)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<PollHub>();
            hubContext.Clients.Group(pollUpdate.PollId.ToString()).updateVotes(pollUpdate);
        }

        public void LinkConnectionToPollUpdates(int pollId)
        {
            LinkConnectionToPollUpdates(pollId, Context.ConnectionId);
        }

        public void LinkConnectionToPollUpdates(int pollId, string connectionId)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<PollHub>();
            
            hubContext.Groups.Add(connectionId, pollId.ToString());
        }

        public void StartPoll(int pollId)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<PollHub>();
            hubContext.Clients.Group(pollId.ToString()).pollStarted(pollId);
        }

        public async Task JoinPollGroup(int pollId)
        {
            await Groups.Add(Context.ConnectionId, pollId.ToString());
        }
    }
}