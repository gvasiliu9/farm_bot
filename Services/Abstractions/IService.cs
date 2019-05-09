using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IService<Entity> where Entity : class
    {
        /// <summary>
        /// Add entity
        /// </summary>
        /// <returns></returns>
        Task AddAsync(Entity entity);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="plant"></param>
        /// <returns></returns>
        Task UpdateAsync(Entity entity);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Entity>> GetAllAsync();

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <returns></returns>
        Task<Entity> GetByIdAsync(Guid id);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);
    }
}
