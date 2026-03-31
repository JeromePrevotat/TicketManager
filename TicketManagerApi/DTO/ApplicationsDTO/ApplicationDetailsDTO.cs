namespace TicketManagerApi.DTO.ApplicationsDTO;

public class ApplicationDetailsDTO
{
  public int Id { get; set; }
  public int OwnerId { get; set; }
  public required string Name { get; set; }
  public string? Description { get; set; }
  public List<int>? MembersId { get; set; } = [];
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}
