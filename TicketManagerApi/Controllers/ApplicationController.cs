using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagerApi.Data;
using TicketManagerApi.DTO.ApplicationsDTO;
using TicketManagerApi.Entities;
using TicketManagerApi.Mapper.ApplicationMapper;

namespace TicketManagerApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        [HttpPost(Name = "CreateApplication")]
        public async Task<ActionResult<ApplicationDetailsDTO>> CreateApplication
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
    }
}
