namespace TicketManagerApi.Entities;

public class Application
{
  public int Id { get; set; }
  public int OwnerId { get; set; }
  public required User Owner { get; set; }
  public required string Name { get; set; }
  public string? Description { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public List<Ticket> Tickets { get; set; } = [];
  public List<User> Members { get; set; } = [];
}
