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
            return NoContent();
        }
    
        [Authorize]
        [HttpPut("{id}", Name = "EditApplicationById")]
        public async Task<ActionResult> Edit(
            int id,
            ApplicationEditDTO updatedApplicationDTO,
            TicketManagerContext dbContext
        )
        {
            var application = await dbContext.Applications.FindAsync(id);
            if (application is null) return NotFound();
            await dbContext.Applications
                .Where(app => app.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(
                        a => a.OwnerId,
                        a => (updatedApplicationDTO.OwnerId == null)
                            ? a.OwnerId
                            : updatedApplicationDTO.OwnerId
                    )
                    .SetProperty(
                        a => a.Name,
                        a => String.IsNullOrEmpty(updatedApplicationDTO.Name)
                            ? a.Name
                            : updatedApplicationDTO.Name
                    )
                    .SetProperty(
                        a => a.Description,
                        a => String.IsNullOrEmpty(updatedApplicationDTO.Description)
                            ? a.Description
                            : updatedApplicationDTO.Description
                    )
                    .SetProperty(a => a.UpdatedAt, DateTime.UtcNow)
                );
            return NoContent();
        }
    
        [Authorize]
        [HttpPut("{id}/team", Name = "EditApplicationTeam")]
        public async Task<ActionResult> EditTeam (
            int id,
            ApplicationEditTeamDTO updatedTeamDTO,
            TicketManagerContext dbContext
        )
        {
            var application = await dbContext.Applications
                .Include(app => app.Members)
                .FirstOrDefaultAsync(app => app.Id == id);
            if (application is null) return NotFound();
            var updatedTeam = await dbContext.Users
                                        .Where(u => updatedTeamDTO.MembersId.Contains(u.Id))
                                        .ToListAsync();
            application.Members.Clear();
            application.Members.AddRange(updatedTeam);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
