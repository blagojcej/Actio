using Actio.Common.Commands;
using Actio.Services.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        private async Task<IActionResult> Login([FromBody] AuthenticateUserCommand command)
            =>  new JsonResult(await _userService.LoginAsync(command.Email, command.Password));
    }
}