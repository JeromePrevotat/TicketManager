using TicketManagerApi.Entities;

namespace TicketManagerApi.DTO.TicketsDTO;

public class TicketEditDTO
{
  public string? Title { get; set; }
  public string? Content { get; set; }
  public TicketSeverity? Severity { get; set; }
  public TicketStatus? Status { get; set; }
  public List<int>? AssignedTo { get; set; }
  public List<int>? UpvotedBy { get; set; }
  public List<int>? FollowedBy { get; set; }
  public List<int>? AttachmentsId { get; set; }
}
