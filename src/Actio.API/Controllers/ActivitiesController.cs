using Actio.API.Repositories;
using Actio.Common.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ActivitiesController : ControllerBase
    {
        private readonly IBusClient _busClient;
        private readonly IActivityRepository _activityRepository;

        public ActivitiesController(IBusClient busClient, IActivityRepository activityRepository)
        {
            _busClient = busClient;
            _activityRepository = activityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var activities = await _activityRepository.BrowseAsync(Guid.Parse(User.Identity.Name));

            return new JsonResult(activities.Select(x => new {x.Id, x.Name, x.Category, x.CreatedAt}));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var activity = await _activityRepository.GetAsync(id);
            if (activity == null)
                return NotFound();
            if (activity.UserId != Guid.Parse(User.Identity.Name))
                return Unauthorized();

            return new JsonResult(activity);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateActivityCommand command)
        {
            //Set new guid and cretedAt on the server
            command.Id=Guid.NewGuid();
            command.CreatedAt=DateTime.UtcNow;
            //Set UserId based on User Identity Name
            command.UserId = Guid.Parse(User.Identity.Name);
            await _busClient.PublishAsync(command);

            return Accepted($"activities/{command.Id}");
        }
    }
}