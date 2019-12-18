using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.API.Models;
using Actio.API.Repositories;
using Actio.Common.Events;

namespace Actio.API.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreatedEvent>
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityCreatedHandler(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task HandleAsync(ActivityCreatedEvent @event)
        {
            await _activityRepository.AddAsync(new Activity()
            {
                Category = @event.Category,
                Id = @event.Id,
                CreatedAt = @event.CreatedAt,
                UserId = @event.UserId,
                Description = @event.Description,
                Name = @event.ActivityName
            });
            Console.WriteLine($"Activity created: {@event.ActivityName}");
        }
    }
}
