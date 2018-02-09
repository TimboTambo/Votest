using System;
using System.Collections.Generic;
using System.Linq;
using Voting.Hubs;
using Voting.Services.Implementation.Repositories;
using Voting.Services.Models;

namespace Voting.Services.Implementation.Services
{
    public class PollService : IPollService
    {
        private readonly IPollRepository _pollRepository;
        private readonly IPollHub _pollHub;

        public PollService(IPollRepository pollRepository, IPollHub pollHub)
        {
            _pollRepository = pollRepository;
            _pollHub = pollHub;
        }

        public int SavePoll(CreatePollModel poll, Guid userId)
        {
            var pollDb = MapToDbObject(poll, userId);
            return _pollRepository.SavePoll(pollDb);
        }

        private Poll MapToDbObject(CreatePollModel poll, Guid userId)
        {
            var pollDb = new Poll
            {
                DateCreated = DateTime.UtcNow,
                PollStateId = (int)PollState.Created,
                CreatedBy = userId,
                Questions = MapToDbObject(poll.Questions),
                PollName = poll.PollName,
                DurationSeconds = poll.DurationMinutes * 60,
                LinkCode = Poll.RandomString(3),
                Description = poll.Description
            };
            return pollDb;
        }

        private List<Question> MapToDbObject(List<NewQuestion> questions)
        {
            var questionsDb = new List<Question>();
            foreach (var question in questions)
            {
                var questionDb = new Question
                {
                    QuestionText = question.QuestionText,
                    QuestionTypeId = (int)question.QuestionType,
                    Options = MapToDbObject(question.Options)
                };
                questionsDb.Add(questionDb);
            }
            return questionsDb;
        }

        private List<Option> MapToDbObject(List<NewQuestionOption> options)
        {
            var optionsDb = new List<Option>();
            foreach (var option in options)
            {
                var optionDb = new Option
                {
                    OptionText = option.OptionText
                };
                optionsDb.Add(optionDb);
            }
            return optionsDb;
        }

        public void SaveResponse(CompletePollModel response)
        {
            var questionResponses = response.Questions.Select(MapToDbObject).ToList();
            var responseDb = new Response
            {
                UserId = Guid.NewGuid(),
                DateSubmitted = DateTime.UtcNow,
                PollId = response.PollId,
                QuestionResponses = questionResponses
            };
            _pollRepository.SaveResponse(responseDb);
            _pollRepository.UpdateVotes(response.PollId, questionResponses);

            var questionUpdates = new List<QuestionRealtimeUpdate>();
            foreach (var question in questionResponses)
            {
                questionUpdates.Add(new QuestionRealtimeUpdate
                {
                    QuestionId = question.QuestionId,
                    OptionId = question.OptionId
                });
            }
            var pollUpdate = new PollRealtimeUpdate
            {
                PollId = response.PollId,
                QuestionUpdates = questionUpdates
            };
            _pollHub.UpdateVotes(pollUpdate);
        }

        public QuestionResponse MapToDbObject(CompletePollQuestion question)
        {
            return new QuestionResponse
            {
                OptionId = question.ResponseId,
                QuestionId = question.QuestionId,
                PollId = question.PollId,
                QuestionTypeId = (int)question.QuestionType
            };
        }

        public Poll GetPoll(string searchTerm)
        {
            return _pollRepository.GetPoll(searchTerm);
        }

        public Poll GetPoll(int pollId)
        {
            return _pollRepository.GetPoll(pollId);
        }

        public Poll StartPoll(int pollId)
        {
            var startedPoll = _pollRepository.StartPoll(pollId);
            _pollHub.StartPoll(pollId);
            return startedPoll;
        }

        public IQueryable<Poll> GetAllUsersPolls(Guid userId)
        {
            return _pollRepository.GetAllUserPolls(userId);
        }
    }
}