using System;
using System.Collections.Generic;
using System.Text;
using Actio.Common.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RawRabbit;
using RawRabbit.Instantiation;

namespace Actio.Common.Mongo
{
    public static class Extensions
    {
        public static void AddMongoDB(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<MongoOptions>(configuration.GetSection("mongo"));
            serviceCollection.AddSingleton<MongoClient>(c =>
            {
                var options = c.GetService<IOptions<MongoOptions>>();

                return new MongoClient(options.Value.ConnectionString);
            });
            serviceCollection.AddScoped<IMongoDatabase>(c =>
            {
                var options = c.GetService<IOptions<MongoOptions>>();
                var client = c.GetService<MongoClient>();

                return client.GetDatabase(options.Value.Database);
            });

            serviceCollection.AddScoped<IDatabaseInitializer, MongoInitializer>();
            serviceCollection.AddScoped<IDatabaseSeeder, MongoSeeder>();
        }
    }
}
