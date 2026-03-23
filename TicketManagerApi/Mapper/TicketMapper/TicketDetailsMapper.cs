using TicketManagerApi.DTO.TicketsDTO;
using TicketManagerApi.Entities;

namespace TicketManagerApi.Mapper.TicketMapper;

public static class TicketDetailsMapper
{
  public static TicketDetailsDTO ToDTO(this Ticket ticket)
  {
    return new TicketDetailsDTO
    {
      Id = ticket.Id,
      ApplicationId = ticket.ApplicationId,
      AuthorId = ticket.AuthorId,
      CreatedAt = ticket.CreatedAt,
      UpdatedAt = ticket.UpdatedAt,
      Title = ticket.Title,
      Description = ticket.Description,
      Severity = ticket.Severity,
      Status = ticket.Status
    };
  }
}
