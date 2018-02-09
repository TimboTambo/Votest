using System;
using System.Collections.Generic;

namespace Voting.Services.Models
{
    public class WaitingOnPollModel
    {
        public DateTime DateCreated { get; set; }
        public string LinkCode { get; set; }
        public int PollId { get; set; }
        public string PollName { get; set; }
        public bool UserIsCreator { get; set; }
        public string Description { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}