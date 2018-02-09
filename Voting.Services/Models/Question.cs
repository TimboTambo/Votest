using System.Collections.Generic;
using System.Linq;
using Voting.Models;

namespace Voting.Services.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public int PollId { get; set; }
        public string QuestionText { get; set; }
        public int QuestionTypeId { get; set; }
        public virtual ICollection<Option> Options { get; set; } = new List<Option>();

        public CompletePollQuestion MapToViewObject()
        {
            return new CompletePollQuestion
            {
                PollId = PollId,
                QuestionId = QuestionId,
                QuestionType = (QuestionType)QuestionTypeId,
                QuestionText = QuestionText,
                Options = Options.ToList(),
            };
        }

        public QuestionResultsModel MapToResultsModel()
        {
            return new QuestionResultsModel
            {
                PollId = PollId,
                QuestionId = QuestionId,
                QuestionText = QuestionText,
                QuestionTypeId = QuestionTypeId,
                Options = Options.Select(o => o.MapToResultsModel()).ToList()
            };
        }
    }
}