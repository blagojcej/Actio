using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Events;

namespace Actio.API.Handlers
{
    public class UserAuthenticatedHandler : IEventHandler<UserAuthenticatedEvent>
    {
        public async Task HandleAsync(UserAuthenticatedEvent @event)
        {
            await Task.CompletedTask;
            Console.WriteLine($"User authenticated: {@event.Email}");
        }
    }
}
