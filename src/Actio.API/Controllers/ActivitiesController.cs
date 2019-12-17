using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Actio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IBusClient _busClient;

        public ActivitiesController(IBusClient busClient)
        {
            _busClient = busClient;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateActivityCommand command)
        {
            //Set new guid and cretedAt on the server
            command.Id=Guid.NewGuid();
            command.CreatedAt=DateTime.UtcNow;
            await _busClient.PublishAsync(command);

            return Accepted($"activities/{command.Id}");
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Get() => Content("Scured");
    }
}