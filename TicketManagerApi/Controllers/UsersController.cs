using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagerApi.Data;
using TicketManagerApi.DTO.UsersDTO;
using TicketManagerApi.Entities;
using TicketManagerApi.Mapper.UserMapper;

namespace TicketManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [Authorize]
        [HttpGet("me", Name = "GetMe")]
        public async Task<ActionResult<UserSummaryDto>> GetMe (
            TicketManagerContext dbContext
        )
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null) return Unauthorized();
            var currentUser = await dbContext.Users.FindAsync(int.Parse(userId));
            if (currentUser is null) return NotFound();
            return Ok(UserMapper.ToUserSummaryDto(currentUser));
        }
    }
}
