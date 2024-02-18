namespace Repositories
{
    public interface ITriggersBaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
    }
}