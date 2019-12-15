using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Actio.Services.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();

            ServiceHost.Create<Startup>(args)
                .UserRabbitMq()
                //Add as many subscriptions to events/commands as we want
                //Try to grab event handler from our services collection which we defined in Startup.cs
                //Subscribe to it and invoke event hanlder if its found,if it's not found will throw an exception
                .SubscribeToCommand<CreateUserCommand>()
                .Build()
                .Run();
        }

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>();
    }
}
