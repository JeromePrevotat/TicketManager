namespace TicketManagerApi.Entities;

public enum RoleName
{
  ADMIN,
  USER,
}

public class Role
{
  public int Id { get; set; }
  public RoleName RoleName { get; set; }
  
  // Many-to-many relationship with User
  public List<User> UsersRoles { get; set; } = [];
}