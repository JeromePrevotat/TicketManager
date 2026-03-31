using TicketManagerApi.Entities;

namespace TicketManagerApi.DTO.ApplicationsDTO;

public class ApplicationEditDTO
{
  public int? OwnerId { get; set; }
  public string? Name { get; set; }
  public string? Description { get; set; }
}
