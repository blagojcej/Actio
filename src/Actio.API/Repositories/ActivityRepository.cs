using Actio.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.API.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase _mongoDatabase;

        public ActivityRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<Activity> GetAsync(Guid id)
            => await ActivityCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(Activity model)
            => await ActivityCollection.InsertOneAsync(model);

        public async Task<IEnumerable<Activity>> BrowseAsync(Guid userId)
            => await ActivityCollection.AsQueryable().Where(x => x.UserId == userId)
                .ToListAsync();

        private IMongoCollection<Activity> ActivityCollection => _mongoDatabase.GetCollection<Activity>("Activities");
    }
}
