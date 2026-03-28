namespace TicketManagerApi.DTO.ApplicationsDTO;

public class ApplicationCreateDTO
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required int OwnerId { get; set; }
}
