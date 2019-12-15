using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.Services.Activities.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoDatabase _mongoDatabase;

        public CategoryRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<Category> GetAsync(string name)
            => await CategoryCollection.AsQueryable().FirstOrDefaultAsync(x => x.Name == name.ToLowerInvariant());

        public async Task<IEnumerable<Category>> BrowseAsync()
            => await CategoryCollection.AsQueryable().ToListAsync();

        public async Task AddAsync(Category category)
            => await CategoryCollection.InsertOneAsync(category);

        private IMongoCollection<Category> CategoryCollection => _mongoDatabase.GetCollection<Category>("Categories");
    }
}
