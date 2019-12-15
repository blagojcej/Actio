using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Events;

namespace Actio.API.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreatedEvent>
    {
        public async Task HandleAsync(ActivityCreatedEvent @event)
        {
            Console.WriteLine($"Activity created: {@event.ActivityName}");
            await Task.CompletedTask;
        }
    }
}
