using Demo.API.Domain.Entities;
using Demo.API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Demo.API.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = _entities;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query.ToArrayAsync();
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            return _entities.FindAsync(id).AsTask();
        }

        public void Update(TEntity entity)
        {
            _entities.Update(entity);
        }
    }
}
