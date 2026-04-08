using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketManagerApi.DTO.AuthDTO;
using TicketManagerApi.Entities;
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
        public async Task<ActionResult<LoginResponseDTO>> login(
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
    }
}
