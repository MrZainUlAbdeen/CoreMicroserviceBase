using Repositories;

namespace Student.Infrastructure.Interfaces
{
    public interface IStudentRepository : ITriggersBaseRepository<Domain.Student>
    {
    }
}