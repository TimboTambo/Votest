using System.Collections.Generic;

namespace Voting.Services.Models
{
    public class PollRealtimeUpdate
    {
        public int PollId { get; set; }
        public List<QuestionRealtimeUpdate> QuestionUpdates { get; set; }
    }
}