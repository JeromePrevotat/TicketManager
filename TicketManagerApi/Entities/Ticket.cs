namespace TicketManagerApi.Entities;

public class Ticket
{
  public int Id { get; set; }
  public int ApplicationId { get; set; }
  public required Application Application { get; set; }
  public required int AuthorId { get; set; }
  public required User Author { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public required string Title { get; set; }
  public required string Description { get; set; }
  public TicketSeverity Severity { get; set; }
  public TicketStatus Status { get; set; }
  public List<User> AssignedTo { get; set; } = [];
  public List<User> UpvotedBy { get; set; } = [];
  public List<User> FollowedBy { get; set; } = [];
  public List<Attachment> Attachments { get; set; } = [];

}
