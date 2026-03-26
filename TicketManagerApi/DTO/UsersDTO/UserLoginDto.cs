namespace TicketManagerApi.DTO.UsersDTO;

public class UserLoginDto
{
  public string? Username { get; set; }
  public string? Email { get; set; }
  public required string Password { get; set; }
}
