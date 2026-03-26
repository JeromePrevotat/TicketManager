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
        public async Task<IActionResult> Login(
            [FromBody] UserLoginDto userLoginDto)
        {
            var result =await authService.Login(userLoginDto);
            if (result is null)
            {
                return Unauthorized(new ProblemDetails
                {
                    Title = "Invalid credentials",
                    Status = StatusCodes.Status401Unauthorized,
                    Detail = "Invalid Credentials."
                });
            }
            return Ok(result);
        }
        
        [HttpGet("logout", Name = "Logout")]
        public IActionResult Logout()
        {
            return Ok();
        }
    }
}
