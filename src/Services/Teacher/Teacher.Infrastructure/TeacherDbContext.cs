using Microsoft.EntityFrameworkCore;

namespace Teacher.Infrastructure
{
    public class TeacherDbContext : DbContext
    {
        public TeacherDbContext(DbContextOptions<TeacherDbContext> options) : base(options)
        {
        }

        public DbSet<Domain.Teacher> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("teacher");
        }
    }
}