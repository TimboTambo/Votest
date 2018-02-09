using System;
using System.Collections.Generic;

namespace Voting.Services.Models
{
    public class CompletePollModel
    {
        public CompletePollModel()
        {
            
        }

        public int PollId { get; set; }
        public DateTime FinishDate { get; set; }
        public string LinkCode { get; set; }
        public string PollName { get; set; }
        public virtual IEnumerable<CompletePollQuestion> Questions { get; set; }
        public string Description { get; set; }
    }
}