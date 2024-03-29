﻿using Actio.Common.Commands;
using Actio.Common.Mongo;
using Actio.Common.RabbitMQ;
using Actio.Services.Activities.Domain.Repositories;
using Actio.Services.Activities.Handlers;
using Actio.Services.Activities.Repositories;
using Actio.Services.Activities.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Actio.Services.Activities
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();

            services.AddMongoDB(Configuration);

            services.AddRabbitMq(Configuration);

            services.AddScoped<ICommandHandler<CreateActivityCommand>, CreateActivityHandler>();

            //Register repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();

            //Register MongoDB database seeder
            services.AddScoped<IDatabaseSeeder, CustomMongoSeeder>();

            //Register services
            services.AddScoped<IActivityService, ActivityService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var databaseInitializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();

                databaseInitializer.InitializeAsync();
            }

            //app.ApplicationServices.GetService<IDatabaseInitializer>().InitializeAsync();

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
