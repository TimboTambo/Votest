using System;
using System.Collections.Generic;
using System.Linq;
using Voting.Services.Implementation.Contexts;
using Voting.Services.Models;

namespace Voting.Services.Implementation.Repositories.Implementations
{
    public class PollRepository : IPollRepository
    {
        private PollDbContext context = new PollDbContext();

        public PollRepository()
        {

        }

        public Poll GetPoll(string searchText)
        {
            return context.Polls.FirstOrDefault(p => p.LinkCode.Equals(searchText, StringComparison.OrdinalIgnoreCase));
        }

        public int SavePoll(Poll poll)
        {
            var savedPoll = context.Polls.Add(poll);
            context.SaveChanges();
            return savedPoll.PollId;
        }

        public void SaveResponse(Response response)
        {
            context.Responses.Add(response);
            context.SaveChanges();
        }

        public Poll GetPoll(int pollId)
        {
            return context.Polls.FirstOrDefault(p => p.PollId == pollId);
        }

        public Poll StartPoll(int pollId)
        {
            var poll = context.Polls.FirstOrDefault(p => p.PollId == pollId);
            if (poll == null)
            {
                return null;
            }
            poll.Start();
            context.SaveChanges();
            return poll;
        }

        public void UpdateVotes(int pollId, List<QuestionResponse> questionResponses)
        {
            var poll = context.Polls.FirstOrDefault(p => p.PollId == pollId);
            if (poll != null)
            {
                poll.Responses++;
            }
            foreach (var questionResponse in questionResponses)
            {
                var selectedOption = context.Question.FirstOrDefault(q => q.QuestionId == questionResponse.QuestionId)?.Options.FirstOrDefault(o => o.OptionId == questionResponse.OptionId);
                if (selectedOption != null)
                {
                    selectedOption.Votes++;
                }
            }
            context.SaveChanges();
        }

        public IQueryable<Poll> GetAllUserPolls(Guid userId)
        {
            return context.Polls.Where(p => p.CreatedBy == userId);
        }
    }
}