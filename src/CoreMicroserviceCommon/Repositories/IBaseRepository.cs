using System.Linq.Expressions;

namespace Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity Insert(TEntity entity);
        IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null);
        TEntity? GetById(object id);
        Task<TEntity?> GetByIdAsync(object id);
        void Update(TEntity entityToUpdate);
        void Delete(TEntity entityToDelete);
        Task SaveChangesAsync();
        void SaveChanges();
    }
}