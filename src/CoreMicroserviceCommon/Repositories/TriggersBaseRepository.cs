using EntityFrameworkCore.Triggers;

namespace Repositories
{
    public class TriggersBaseRepository<TEntity, TDbContext> :
        BaseRepository<TEntity, TDbContext>,
        ITriggersBaseRepository<TEntity>
        where TEntity : class
        where TDbContext : DbContextWithTriggers
    {
        public TriggersBaseRepository(TDbContext dbContext) : base(dbContext)
        {
        }
    }
}