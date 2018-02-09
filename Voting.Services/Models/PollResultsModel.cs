using System;
using System.Collections.Generic;
using Voting.Models;

namespace Voting.Services.Models
{
    public class PollResultsModel
    {
        public DateTime FinishDate { get; set; }
        public string LinkCode { get; set; }
        public int PollId { get; set; }
        public string PollName { get; set; }
        public List<QuestionResultsModel> Questions { get; set; }
        public int NumberOfResponses { get; set; }
        public string Description { get; set; }
    }
}