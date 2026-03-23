using TicketManagerApi.Entities;

namespace TicketManagerApi.DTO.TicketsDTO;

public class TicketDetailsDTO
{
  public int Id { get; set; }
  public int ApplicationId { get; set; }
  public int AuthorId { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public required string Title { get; set; }
  public required string Description { get; set; }
  public TicketSeverity Severity { get; set; }
  public TicketStatus Status { get; set; }
}
