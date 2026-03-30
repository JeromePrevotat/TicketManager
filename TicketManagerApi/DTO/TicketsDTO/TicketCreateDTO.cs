using TicketManagerApi.Entities;

namespace TicketManagerApi.DTO.TicketsDTO;

public class TicketCreateDTO
{
  public required string Title { get; set; }
  public required string Content { get; set; }
  public required TicketSeverity Severity { get; set; }
  public List<Attachment> Attachments { get; set; } = [];
}
