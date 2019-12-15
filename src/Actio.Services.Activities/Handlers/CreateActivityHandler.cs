using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exceptions;
using Actio.Services.Activities.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Actio.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivityCommand>
    {
        private readonly IBusClient _busClient;
        private readonly IActivityService _activityService;
        private readonly ILogger<CreateActivityHandler> _logger;

        public CreateActivityHandler(IBusClient busClient, IActivityService activityService, ILogger<CreateActivityHandler> logger)
        {
            _busClient = busClient;
            _activityService = activityService;
            _logger = logger;
        }

        public async Task HandleAsync(CreateActivityCommand command)
        {
            //Console.WriteLine($"Creating activity: {command.ActivityName}");
            _logger.LogInformation($"Creating activity: {command.ActivityName}");

            try
            {
                //Creating and saving the activity
                await _activityService.AddAsync(command.Id, command.UserId, command.Category, command.ActivityName,
                    command.Description, command.CreatedAt);

                //Publish message to RabbitMQ bus
                await _busClient.PublishAsync(new ActivityCreatedEvent(command.Id, command.UserId, command.Category,
                    command.ActivityName,
                    command.Description, command.CreatedAt));

                return;
            }
            catch (ActioExcteption actioExcteption)
            {
                //Publish own custom exception
                await _busClient.PublishAsync(new CreateActivityRejected(command.Id, actioExcteption.Message,
                    actioExcteption.Code));
                _logger.LogError(actioExcteption.Message);
            }
            catch (Exception ex)
            {
                //Publish general exception
                await _busClient.PublishAsync(new CreateActivityRejected(command.Id, ex.Message,
                    "error"));
                _logger.LogError(ex.Message);
            }

            
        }
    }
}
