using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voting.Services.Models
{
    public class QuestionResponse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionResponseId { get; set; }
        public int QuestionId { get; set; }
        public int PollId { get; set; }
        public int QuestionTypeId { get; set; }
        public int OptionId { get; set; }
        public int ResponseId { get; set; }
    }
}