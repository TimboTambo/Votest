using System.Collections.Generic;

namespace Voting.Services.Models
{
    public class CompletePollQuestion
    {
        public CompletePollQuestion()
        {
            
        }

        public int PollId { get; set; }
        public int QuestionId { get; set; }
        public int ResponseId { get; set; }
        public QuestionType QuestionType { get; set; }
        public string QuestionText { get; set; }
        public List<Option> Options { get; set; }
        public int QuestionIndex { get; set; }
    }
}