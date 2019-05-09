using Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Services
{
    public class Repository<Entity> : IRepository<Entity> where Entity : BaseEntity
    {
        private readonly DbSet<Entity> _dbSet;

        private readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Entity>();
        }

        public long Count()
            => _dbSet.Count();

        public void Add(Entity entity)
           => _dbSet.Add(entity);

        public void AddRange(IEnumerable<Entity> entities)
           => _dbSet.AddRange(entities);

        public IQueryable<Entity> GetAll()
            => _dbSet.AsQueryable();

        public Entity GetById(Guid? id)
            => _dbSet.FirstOrDefault(e => e.Id == id);

        public void Remove(Entity entity)
            => _dbSet.Remove(entity);

        public void RemoveRange(IEnumerable<Entity> entities)
            => _dbSet.RemoveRange(entities);

        public IQueryable<Entity> Where(Expression<Func<Entity, bool>> predicate)
            => _dbSet.Where(predicate);

        public Entity FirstOrDefault(Expression<Func<Entity, bool>> predicate)
            => _dbSet.FirstOrDefault(predicate);

        public bool Any(Expression<Func<Entity, bool>> predicate)
            => _dbSet.Any(predicate);

        public bool Any()
            => _dbSet.Any();
    }
}
