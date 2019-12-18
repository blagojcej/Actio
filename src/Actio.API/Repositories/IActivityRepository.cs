using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.API.Models;

namespace Actio.API.Repositories
{
    public interface IActivityRepository
    {
        /// <summary>
        /// Function which returns one Activity object by his id
        /// </summary>
        /// <param name="id">Guid of the object</param>
        /// <returns></returns>
        Task<Activity> GetAsync(Guid id);
        /// <summary>
        /// Function which adds an activity to log database
        /// </summary>
        /// <param name="model">activity model</param>
        /// <returns></returns>
        Task AddAsync(Activity model);
        /// <summary>
        /// Function which returns all activities by given user
        /// </summary>
        /// <param name="userId">Id of user (Guid)</param>
        /// <returns></returns>
        Task<IEnumerable<Activity>> BrowseAsync(Guid userId);
    }
}