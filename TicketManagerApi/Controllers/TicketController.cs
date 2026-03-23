using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketManagerApi.Data;
using TicketManagerApi.DTO.TicketsDTO;
using TicketManagerApi.Entities;
using TicketManagerApi.Mapper.TicketMapper;

namespace TicketManagerApi.Controllers
{
    // [Authorize]
    // [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        [HttpGet(Name = "GetAllTickets")]
        public async Task<ActionResult<IEnumerable<TicketDetailsDTO>>> Get(TicketManagerContext dbContext)
        {
            var tickets = await dbContext.Tickets
                    .Include(t => t.ApplicationId)
                    .Include(t => t.AuthorId)
                    .ToListAsync();
            return Ok(
                tickets.Select(t => TicketDetailsMapper.ToDTO(t))
            );
        }
    }
}
