namespace TicketManagerApi.Entities;

public class User
{
  public int Id { get; set; }
  public string? Username { get; set; }
  public required string Email { get; set; }
  public required string Password { get; set; }

  // Many-to-many relationship with Role
  public List<Role> Roles { get; set; } = [];

  public List<Ticket> TicketsCreated { get; set; } = [];
  public List<Ticket> TicketsFollowed { get; set; } = [];
  public List<Ticket> TicketsUpvoted { get; set; } = [];
  public List<Ticket> TicketsAssigned { get; set; } = [];

  public List<Application> MemberOf { get; set; } = [];
  public List<Application> OwnedApplications { get; set; } = [];
}
