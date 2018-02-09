namespace Voting.Models
{
    public class OptionResultsModel
    {
        public int OptionId { get; set; }
        public int QuestionId { get; set; }
        public string OptionText { get; set; }
        public int Votes { get; set; }
    }
}