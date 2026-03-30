using TicketManagerApi.Entities;

namespace TicketManagerApi.DTO.TicketsDTO;

public class TicketEditDTO
{
  public string? Title { get; set; }
  public string? Content { get; set; }
  public TicketSeverity? Severity { get; set; }
  public TicketStatus? Status { get; set; }
  public List<User>? AssignedTo { get; set; }
  public List<User>? UpvotedBy { get; set; }
  public List<User>? FollowedBy { get; set; }
  public List<Attachment>? Attachments { get; set; }
}
