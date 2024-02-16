using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repositories
{
    public class BaseRepository<TEntity, TDbContext> : IBaseRepository<TEntity>
        where TEntity : class
        where TDbContext : DbContextWithTriggers
    {
        public TDbContext _dbContext { get; private set; }
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        private IQueryable<TEntity> GetEntityQueryable(Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            return await GetEntityQueryable(filter).ToListAsync();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null)
        {
            return GetEntityQueryable(filter).ToList();
        }

        public virtual async Task<TEntity?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual TEntity? GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public virtual IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            return entities;
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}