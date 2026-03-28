using TicketManagerApi.DTO.ApplicationsDTO;
using TicketManagerApi.Entities;

namespace TicketManagerApi.Mapper.ApplicationMapper;

public static class ApplicationDetailsMapper
{
  public static ApplicationDetailsDTO ToDTO(this Application app)
  {
    return new ApplicationDetailsDTO
    {
      Id = app.Id,
      OwnerId = app.OwnerId,
      Name = app.Name,
      Description = app.Description,
      CreatedAt = app.CreatedAt,
      UpdatedAt = app.UpdatedAt
    };
  }
}
