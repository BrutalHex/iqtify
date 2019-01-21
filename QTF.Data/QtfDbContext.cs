using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QTF.Data.Models;

namespace QTF.Data
{
    public class QtfDbContext : IdentityDbContext<ApplicationUser>
    {
        public QtfDbContext(DbContextOptions<QtfDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskAnswer> Answers { get; set; }
        public DbSet<QuestTask> QuestTasks { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<QuestRecord> QuestRecords { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }

        public DbSet<Metadata> Metadata { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // set internal name of the task to be unique
            builder.Entity<Quest>().HasAlternateKey(t => t.InternalName);
            builder.Entity<QuestTask>().HasAlternateKey(t => t.InternalName);
        }
    }
}
