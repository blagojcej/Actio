using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Services.Activities.Domain.Models;

namespace Actio.Services.Activities.Domain.Repositories
{
    public interface IActivityRepository
    {
        /// <summary>
        /// Get activity by id
        /// </summary>
        /// <param name="id">Id of activity</param>
        /// <returns></returns>
        Task<Activity> GetAsync(Guid id);

        Task AddAsync(Activity activity);
    }
}