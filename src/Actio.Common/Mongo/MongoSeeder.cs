using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Actio.Common.Mongo
{
    public class MongoSeeder : IDatabaseSeeder
    {
        protected readonly IMongoDatabase _database;

        public MongoSeeder(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task SeedAsync()
        {
            //Allows us to navigate through the collection
            var collectionCursor = await _database.ListCollectionsAsync();

            //If there are any collections, return
            var collection = await collectionCursor.ToListAsync();
            if(collection.Any()) return;

            //Seed database
            await CustomSeedAsync();
        }

        protected virtual async Task CustomSeedAsync()
        {
            //Default implementation does nothing, create/override custom implementation
            await Task.CompletedTask;
        }
    }
}
