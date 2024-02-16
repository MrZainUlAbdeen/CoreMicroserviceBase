using Repositories;
using Student.Infrastructure.Interfaces;

namespace Student.Infrastructure.Repositories
{
    public class StudentRepository : BaseRepository<Domain.Student, StudentDbContext>, IStudentRepository
    {
        public StudentRepository(StudentDbContext dbContext) : base(dbContext)
        {
        }
    }
}