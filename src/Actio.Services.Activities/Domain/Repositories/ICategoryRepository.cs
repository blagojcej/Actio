using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Services.Activities.Domain.Models;

namespace Actio.Services.Activities.Domain.Repositories
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// Get category by name
        /// </summary>
        /// <param name="name">Name of the category</param>
        /// <returns></returns>
        Task<Category> GetAsync(string name);

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Category>> BrowseAsync();

        Task AddAsync(Category category);
    }
}