using Repositories;
using Student.Infrastructure.Interfaces;

namespace Student.Infrastructure.Repositories
{
    public class StudentRepository : TriggersBaseRepository<Domain.Student, StudentDbContext>, IStudentRepository
    {
        public StudentRepository(StudentDbContext dbContext) : base(dbContext)
        {
        }
    }
}