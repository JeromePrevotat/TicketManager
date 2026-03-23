namespace TicketManagerApi.Entities;

public class RefreshToken
{
  public int Id { get; set; }
  public required int OwnerId { get; set; }
  public required string Token { get; set; }
  public required DateTime ExpiresAt { get; set; }
}
