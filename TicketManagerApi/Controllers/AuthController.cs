using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagerApi.Data;
using TicketManagerApi.DTO.UsersDTO;
using TicketManagerApi.Services;

namespace TicketManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController (AuthService authService)
        : ControllerBase
    {
        [HttpPost("register", Name = "Register")]
        public async Task<IActionResult> Register(
            [FromBody] UserRegisterDto userRegisterDto)
        {
            var newUser = await authService.Register(userRegisterDto);
            return Created($"/api/users/{newUser.Id}", newUser);
        }
        
        [HttpPost("login", Name = "Login")]
        public IActionResult Login()
        {
            return Ok();
        }
        
        [HttpGet("logout", Name = "Logout")]
        public IActionResult Logout()
        {
            return Ok();
        }
    }
}
