using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketManagerApi.Data;
using TicketManagerApi.DTO.TicketsDTO;
using TicketManagerApi.Mapper.TicketMapper;

namespace TicketManagerApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        [HttpGet(Name = "GetAllTickets")]
        public async Task<ActionResult<IEnumerable<TicketDetailsDTO>>> Get(TicketManagerContext dbContext)
        {              
            var tickets = await dbContext.Tickets
                    .ToListAsync();
            return Ok(
                tickets.Select(t => TicketDetailsMapper.ToDTO(t))
            );
        }
    }
}
