using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voting.Services.Models
{
    public class Response
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResponseId { get; set; }
        public int PollId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateSubmitted { get; set; }
        public virtual ICollection<QuestionResponse> QuestionResponses { get; set; }
    }
}