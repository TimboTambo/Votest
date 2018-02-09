using Voting.Models;

namespace Voting.Services.Models
{
    public class Option
    {
        public int OptionId { get; set; }
        public int QuestionId { get; set; }
        public string OptionText { get; set; }
        public int Votes { get; set; }

        public OptionResultsModel MapToResultsModel()
        {
            return new OptionResultsModel
            {
                OptionId = OptionId,
                OptionText = OptionText,
                QuestionId = QuestionId,
                Votes = Votes
            };
        }
    }
}