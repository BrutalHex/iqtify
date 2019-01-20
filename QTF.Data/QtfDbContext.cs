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

        public DbSet<QuestTaskAnswer> Answers { get; set; }
        public DbSet<QuestTask> QuestTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // set internal name of the task to be unique
            builder.Entity<QuestTask>().HasAlternateKey(t => t.InternalName);
        }
    }
}
