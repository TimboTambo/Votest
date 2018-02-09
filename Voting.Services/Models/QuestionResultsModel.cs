using System.Collections.Generic;

namespace Voting.Models
{
    public class QuestionResultsModel
    {
        public int QuestionId { get; set; }
        public int PollId { get; set; }
        public string QuestionText { get; set; }
        public int QuestionTypeId { get; set; }
        public List<OptionResultsModel> Options { get; set; }
    }
}