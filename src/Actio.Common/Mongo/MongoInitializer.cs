﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Actio.Common.Mongo
{
    public class MongoInitializer : IDatabaseInitializer
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IDatabaseSeeder _databaseSeeder;
        private bool _initialized;
        private readonly bool _seed;

        public MongoInitializer(IMongoDatabase mongoDatabase, IOptions<MongoOptions> options, IDatabaseSeeder databaseSeeder)
        {
            _mongoDatabase = mongoDatabase;
            _databaseSeeder = databaseSeeder;
            _seed = options.Value.Seed;
        }

        public async Task InitializeAsync()
        {
            if(_initialized) return;

            RegisterConventions();
            _initialized = true;

            if(!_seed) return;

            //Seed database
            await _databaseSeeder.SeedAsync();
        }

        private void RegisterConventions()
        {
            ConventionRegistry.Register("ActioConventions", new MongoConvention(), x=> true);
        }

        private class MongoConvention : IConventionPack
        {
            public IEnumerable<IConvention> Conventions  => new List<IConvention>()
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }
    }
}
