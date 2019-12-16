using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exceptions;
using Actio.Services.Identity.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Actio.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly ILogger _logger;
        private readonly IBusClient _busClient;
        private readonly IUserService _userService;

        public CreateUserHandler(IBusClient busClient,
            IUserService userService,
            ILogger<CreateUserCommand> logger)
        {
            _busClient = busClient;
            _userService = userService;
            _logger = logger;
        }

        public async Task HandleAsync(CreateUserCommand command)
        {
            _logger.LogInformation($"Creating user: '{command.Email}' with name: '{command.Username}'.");
            try
            {
                await _userService.RegisterAsync(command.Email, command.Password, command.Username);
                await _busClient.PublishAsync(new UserCreatedEvent(command.Email, command.Username));
                _logger.LogInformation($"User: '{command.Email}' was created with name: '{command.Username}'.");

                return;
            }
            catch (ActioExcteption ex)
            {
                _logger.LogError(ex, ex.Message);
                await _busClient.PublishAsync(new CreateUserRejectedEvent(command.Email,
                    ex.Message, ex.Code));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await _busClient.PublishAsync(new CreateUserRejectedEvent(command.Email,
                    ex.Message, "error"));
            }
        }
    }
}
