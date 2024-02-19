using Repositories;
using Teacher.Infrastructure.Interfaces;

namespace Teacher.Infrastructure.Repositories
{
    public class TeacherRepository : BaseRepository<Domain.Teacher, TeacherDbContext>, ITeacherRepository
    {
        public TeacherRepository(TeacherDbContext dbContext) : base(dbContext)
        {
        }
    }
}