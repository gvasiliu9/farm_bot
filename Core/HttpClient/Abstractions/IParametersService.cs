using Entites;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IParametersService
    {
        /// <summary>
        /// Add FarmBot current parameters
        /// </summary>
        /// <param name="currentParameters"></param>
        /// <returns></returns>
        Task AddAsync(Parameters currentParameters);

        /// <summary>
        /// Update FarmBot current paramters
        /// </summary>
        /// <param name="farmBotId"></param>
        /// <returns></returns>
        Task UpdateAsync(Parameters currentParameters);

        /// <summary>
        /// Get FarmBot current paramters
        /// </summary>
        /// <param name="farmBotId"></param>
        /// <returns></returns>
        Task<Parameters> GetByIdAsync(Guid farmBotId);

        /// <summary>
        /// Get all current parameters
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Parameters>> GetAllAsync();

        /// <summary>
        /// Delete FarmBot current parameters
        /// </summary>
        /// <param name="farmBotId"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid farmBotId);
    }
}
