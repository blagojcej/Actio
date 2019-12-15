using System;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Services
{
    public interface IActivityService
    {
        Task AddAsync(Guid id, Guid userId, string categoryName, string name, string description, DateTime createdAt);
    }
}