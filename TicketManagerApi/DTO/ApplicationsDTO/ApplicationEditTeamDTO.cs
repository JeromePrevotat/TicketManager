using TicketManagerApi.Entities;

namespace TicketManagerApi.DTO.ApplicationsDTO;

public class ApplicationEditTeamDTO
{
  public required List<int> MembersId { get; set; } = [];
}
