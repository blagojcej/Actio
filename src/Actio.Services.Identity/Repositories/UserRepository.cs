using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.Services.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;

        public UserRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<User> GetUserAsync(Guid id)
            => await UserCollection
                .AsQueryable()
                .FirstOrDefaultAsync(u => u.Id == id);

        public async Task<User> GetUserAsync(string email)
            => await UserCollection
                .AsQueryable()
                .FirstOrDefaultAsync(u => u.Email == email);

        public async Task AddAsync(User user)
            => await UserCollection.InsertOneAsync(user);

        private IMongoCollection<User> UserCollection
            => _database.GetCollection<User>("Users");
    }
}
