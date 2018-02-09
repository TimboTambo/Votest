using System.Collections.Generic;

namespace Voting.Services.Models
{
    public class CreatePollModel
    {
        public CreatePollModel()
        {
            Questions = new List<NewQuestion> { new NewQuestion()};
            DurationMinutes = 60;
        }
        public string PollName { get; set; }
        public List<NewQuestion> Questions { get; set; }
        public int DurationMinutes { get; set; }
        public string Description { get; set; }
    }
}