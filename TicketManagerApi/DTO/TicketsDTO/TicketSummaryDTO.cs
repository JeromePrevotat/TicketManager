using TicketManagerApi.Entities;

namespace TicketManagerApi.DTO.TicketsDTO;

public class TicketSummaryDTO
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public required string Title { get; set; }
  public TicketSeverity Severity { get; set; }
  public TicketStatus Status { get; set; }
}
