using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Events;

namespace Actio.API.Handlers
{
    public class UserCreatedHandler : IEventHandler<UserCreatedEvent>
    {
        public async Task HandleAsync(UserCreatedEvent @event)
        {
            await Task.CompletedTask;
            Console.WriteLine($"User created: {@event.Email}");
        }
    }
}
