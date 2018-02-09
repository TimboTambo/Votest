namespace Voting.Services.Models
{
    public class QuestionRealtimeUpdate
    {
        public int QuestionId { get; set; }
        public int OptionId { get; set; }
        public int Votes { get; set; }
    }
}