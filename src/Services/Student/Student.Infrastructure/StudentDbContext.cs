using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;

namespace Student.Infrastructure
{
    public class StudentDbContext : DbContextWithTriggers
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }

        public DbSet<Domain.Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("student");
        }
    }
}