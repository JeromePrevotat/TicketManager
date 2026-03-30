using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketManagerApi.Data;
using TicketManagerApi.DTO.TicketsDTO;
using TicketManagerApi.Entities;
using TicketManagerApi.Mapper.TicketMapper;

namespace TicketManagerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
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

        [Authorize]
        [HttpPost("/api/applications/{appId}/tickets", Name = "CreateTicket")]
        public async Task<ActionResult<TicketDetailsDTO>> Create (
            int appId,
            TicketCreateDTO newTicketDTO,
            TicketManagerContext DbContext
        )
        {
            var application = await DbContext.Applications.FindAsync(appId);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (application is null)
                return NotFound();
            if (userId is null)
                return Unauthorized();
            var user = await DbContext.Users.FindAsync(int.Parse(userId));
            if (user is null)
                return Unauthorized();
            
            Ticket newTicket = new()
            {
                ApplicationId = application.Id,
                Application = application,
                AuthorId = user.Id,
                Author = user,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Title = newTicketDTO.Title,
                Content = newTicketDTO.Content,
                Severity = newTicketDTO.Severity,
                Status = TicketStatus.Unconfirmed,
                AssignedTo = [],
                UpvotedBy = [],
                FollowedBy = [],
                Attachments = newTicketDTO.Attachments
            };
            await DbContext.AddAsync(newTicket);
            await DbContext.SaveChangesAsync();
            
            return CreatedAtRoute(
                "GetTicketById",
                new { id = newTicket.Id },
                TicketDetailsMapper.ToDTO(newTicket)
            );
        }

        [HttpGet("{id}", Name = "GetTicketById")]
        public async Task<ActionResult<TicketDetailsDTO>> GetById(
            int id,
            TicketManagerContext dbContext
        )
        {
            var ticket = await dbContext.Tickets.FindAsync(id);
            if (ticket is null)
                return NotFound();
            return Ok(TicketDetailsMapper.ToDTO(ticket));
        }

        [Authorize]
        [HttpPut("{id}", Name = "EditTicketById")]
        public async Task<ActionResult> Edit (
            int id,
            TicketEditDTO updatedTicketDTO,
            TicketManagerContext dbContext
        )
        {
            var ticket = await dbContext.Tickets.FindAsync(id);
            if (ticket is null)
                return NotFound();
            await dbContext.Tickets
                .Where(t => t.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(
                        t => t.Title,
                        t => String.IsNullOrEmpty(updatedTicketDTO.Title)
                            ? t.Title
                            : updatedTicketDTO.Title
                    )
                    .SetProperty(
                        t => t.Content,
                        t => String.IsNullOrEmpty(updatedTicketDTO.Content)
                            ? t.Content
                            : updatedTicketDTO.Content
                    )
                    .SetProperty(
                        t => t.Severity,
                        t => (updatedTicketDTO.Severity == null)
                            ? t.Severity
                            : updatedTicketDTO.Severity
                    )
                    .SetProperty(
                        t => t.Status,
                        t => (updatedTicketDTO.Status == null)
                            ? t.Status
                            : updatedTicketDTO.Status
                    )
                    .SetProperty(t => t.UpdatedAt, DateTime.UtcNow)
                );

            if (updatedTicketDTO.AssignedTo != null)
            {
                ticket.AssignedTo.Clear();
                ticket.AssignedTo.AddRange(updatedTicketDTO.AssignedTo);
            }
            if (updatedTicketDTO.UpvotedBy != null)
            {
                ticket.UpvotedBy.Clear();
                ticket.UpvotedBy.AddRange(updatedTicketDTO.UpvotedBy);
            }
            if (updatedTicketDTO.FollowedBy != null)
            {
                ticket.FollowedBy.Clear();
                ticket.FollowedBy.AddRange(updatedTicketDTO.FollowedBy);
            }
            if (updatedTicketDTO.Attachments != null)
            {
                ticket.Attachments.Clear();
                ticket.Attachments.AddRange(updatedTicketDTO.Attachments);
            }
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}", Name = "DeleteTicketById")]
        public async Task<ActionResult> Delete(
            int id,
            TicketManagerContext dbContext
        )
        {
            var ticket = await dbContext.Tickets.FindAsync(id);
            if (ticket is not null)
            {
                await dbContext.Tickets
                    .Where(t => t.Id == id)
                    .ExecuteDeleteAsync();
            }
            return NoContent();
        }
    }
}
