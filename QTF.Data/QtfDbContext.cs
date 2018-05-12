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

        public DbSet<Quiz> Quizes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionAnswer> Answers { get; set; }
        public DbSet<QtfTask> QtfTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // set internal name of the task to be unique
            builder.Entity<QtfTask>().HasAlternateKey(t => t.InternalName);
        }
    }
}
