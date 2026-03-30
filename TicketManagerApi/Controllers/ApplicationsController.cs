using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketManagerApi.Data;
using TicketManagerApi.DTO.ApplicationsDTO;
using TicketManagerApi.Entities;
using TicketManagerApi.Mapper.ApplicationMapper;

namespace TicketManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        [Authorize]
        [HttpPost(Name = "CreateApplication")]
        public async Task<ActionResult<ApplicationDetailsDTO>> Create
        (
            ApplicationCreateDTO newAppDto,
            TicketManagerContext dbContext
        )
        {
            var request = HttpContext.Request;
            // Validation
            User? Owner = await dbContext.Users.FindAsync(newAppDto.OwnerId);
            if (Owner is null) return NotFound();

            // Creation
            Application newApp = new()
            {
                OwnerId = newAppDto.OwnerId,
                Owner = Owner,
                Name = newAppDto.Name,
                Description = newAppDto.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Tickets = [],
                Members = []
            };
            await dbContext.Applications.AddAsync(newApp);
            await dbContext.SaveChangesAsync();
            
            return CreatedAtRoute(
                "GetApplicationById",
                new { id = newApp.Id },
                ApplicationDetailsMapper.ToDTO(newApp)
            );
        }

        [HttpGet("{id}", Name = "GetApplicationById")]
        public async Task<ActionResult<ApplicationDetailsDTO>> GetById(
            int id,
            TicketManagerContext dbContext
        )
        {
            var application = await dbContext.Applications.FindAsync(id);
            if (application is null) return NotFound();

            return ApplicationDetailsMapper.ToDTO(application);
        }

        [HttpGet(Name = "GetAllApplications")]
        public async Task<ActionResult<ApplicationDetailsDTO>> GetAllApplications(
            TicketManagerContext dbContext
        )
        {
            var applications = await dbContext.Applications.ToListAsync();
            return Ok(applications.Select(app => ApplicationDetailsMapper.ToDTO(app)));
        }

        [Authorize]
        [HttpDelete("{id}", Name = "DeleteApplicationById")]
        public async Task<ActionResult> Delete(
            int id,
            TicketManagerContext dbContext
        )
        {
            var application = await dbContext.Applications.FindAsync(id);
            if (application is not null)
            {
                await dbContext.Applications
                    .Where(app => app.Id == id)
                    .ExecuteDeleteAsync();
            }
            return Ok();
        }
    
        [Authorize]
        [HttpPut("{id}", Name = "UpdateApplicationById")]
        public async Task<ActionResult> Edit(
            int id,
            ApplicationDetailsDTO updatedApplicationDTO,
            TicketManagerContext dbContext
        )
        {
            var application = await dbContext.Applications.FindAsync(id);
            if (application is null)
            {
                return NotFound();
            }
            await dbContext.Applications
                .Where(app => app.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(a => a.OwnerId, updatedApplicationDTO.OwnerId)
                    .SetProperty(a => a.Name, updatedApplicationDTO.Name)
                    .SetProperty(a => a.Description, updatedApplicationDTO.Description)
                    .SetProperty(a => a.UpdatedAt, DateTime.UtcNow)
                );
            return NoContent();
        }
    }
}
