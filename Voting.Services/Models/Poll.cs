using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Voting.Services.Models
{
    public class Poll
    {
        public Poll()
        {
        }

        public static string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public bool IsExpired
        {
            get { return DateStarted.HasValue && DateStarted.Value.AddSeconds(DurationSeconds) < DateTime.UtcNow; }
        }

        public bool IsNotStarted
        {
            get { return DateStarted == null; }
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PollId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateStarted { get; set; }
        public DateTime? DateFinished { get; set; }
        public int DurationSeconds { get; set; }
        public Guid CreatedBy { get; set; }
        public int PollStateId { get; set; }
        public string LinkCode { get; set; }
        public string PollName { get; set; }
        public int Responses { get; set; }
        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
        public string Description { get; set; }

        public void Start()
        {
            DateStarted = DateTime.UtcNow;
            PollStateId = (int) PollState.Started;
        }

        public WaitingOnPollModel GetWaitingOnPollModel(bool currentUserIsCreator = false)
        {
            return new WaitingOnPollModel
            {
                PollId = PollId,
                DateCreated = DateCreated,
                LinkCode = LinkCode,
                PollName = PollName,
                UserIsCreator = currentUserIsCreator,
                Description = Description,
                Questions = Questions
            };
        }

        public CompletePollModel GetCompletePollModel()
        {
            return new CompletePollModel
            {
                PollId = PollId,
                FinishDate = DateFinished ?? DateStarted?.AddSeconds(DurationSeconds) ?? DateTime.UtcNow,
                LinkCode = LinkCode,
                PollName = PollName,
                Questions = Questions.Select(m => m.MapToViewObject()),
                Description = Description
            };
        }

        public PollResultsModel GetPollResultsModel()
        {
            return new PollResultsModel
            {
                FinishDate = DateFinished ?? DateStarted?.AddSeconds(DurationSeconds) ?? DateTime.UtcNow,
                LinkCode = LinkCode,
                PollId = PollId,
                PollName = PollName,
                Questions = Questions.Select(q => q.MapToResultsModel()).ToList(),
                NumberOfResponses = Responses,
                Description = Description
            };
        }
    }
}