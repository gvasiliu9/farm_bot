using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Services
{
    public interface IRepository<Entity>
    {
        long Count();

        void Add(Entity entity);

        void AddRange(IEnumerable<Entity> entities);

        IQueryable<Entity> GetAll();

        Entity GetById(Guid? id);

        void Remove(Entity entity);

        void RemoveRange(IEnumerable<Entity> entities);

        IQueryable<Entity> Where(Expression<Func<Entity, bool>> predicate);

        Entity FirstOrDefault(Expression<Func<Entity, bool>> predicate);

        bool Any(Expression<Func<Entity, bool>> predicate);

        bool Any();
    }
}
