using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Voting.Services.Models;

namespace Voting.Services.Implementation.Contexts
{
    public class PollDbContext : DbContext
    {
        public PollDbContext(): base("Voting")
        {

        }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<QuestionResponse> QuestionResponses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}