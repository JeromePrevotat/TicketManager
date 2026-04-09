using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketManagerApi.DTO.AuthDTO;
using TicketManagerApi.Entities;
using TicketManagerApi.Mapper.UserMapper;
using TicketManagerApi.Services;

namespace TicketManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController (
        UserManager<User> userManager,
        JwtService jwtService
    ): ControllerBase
    {
        [HttpPost("/login", Name = "Login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(
            LoginRequestDTO loginRequestDTO
        )
        {
            var user = await userManager.FindByEmailAsync(loginRequestDTO.Email);
            if (user is null || !await userManager.CheckPasswordAsync(user, loginRequestDTO.Password))
            {
                return Unauthorized("Invalid credentials");
            }
            var accessToken = jwtService.GenerateAccessToken(user);
            var refreshToken = JwtService.GenerateRefreshToken();
            var validUntil = DateTime.UtcNow.AddDays(30);
            
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = validUntil;
            await userManager.UpdateAsync(user);

            Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = validUntil
            });
            
            return Ok(new LoginResponseDTO { AccessToken = accessToken });
        }

        [HttpPost("/register", Name = "Register")]
        public async Task<ActionResult> Register (
            RegisterRequestDTO registerRequestDTO
        )
        {
            var existingUser = await userManager.FindByEmailAsync(registerRequestDTO.Email);
            if (existingUser is not null)
            {
                return Conflict("Email already taken");
            }
            var newUser = new User {
                Email = registerRequestDTO.Email,
                UserName = registerRequestDTO.Email
            };
            var result = await userManager.CreateAsync(newUser, registerRequestDTO.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return CreatedAtRoute(
                routeName: "GetUserById",
                routeValues: new { id = newUser.Id },
                value: UserMapper.ToUserSummaryDto(newUser)
            );
        }
    }
}
