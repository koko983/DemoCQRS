using Demo.API.Domain.Entities;
using System.Linq.Expressions;

namespace Demo.API.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }

    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
