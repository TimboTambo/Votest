using System.Collections.Generic;

namespace Voting.Services.Models
{
    public class NewQuestion
    {
        public NewQuestion()
        {
            Options = new List<NewQuestionOption>
            {
                new NewQuestionOption(),
                new NewQuestionOption()
            };
        }

        public string QuestionText { get; set; }
        public QuestionType QuestionType { get; set; }
        public List<NewQuestionOption> Options {get;set;}
    }
}