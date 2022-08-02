using DataBridge.Domain.Entities;
using DataBridge.Infrastructure.Repository.Interfaces;
using DataBridge.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataBridge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUser _user;
        private readonly TokenServices _token;
        public AuthenticationController(IUser user, TokenServices tokenService)
        {
            _user = user;
            _token = tokenService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User userModel)
        {
            var username = await _user.GetAsync(userModel.Username, userModel.Password);

            if (username == null)
                return NotFound(new { message = "User/Password invalid" });

            var token = _token.GenerateToken(username);
            return new { user = username, token = token };
        }
    }
}
