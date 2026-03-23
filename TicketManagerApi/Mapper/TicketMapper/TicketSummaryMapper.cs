using TicketManagerApi.DTO.TicketsDTO;
using TicketManagerApi.Entities;

namespace TicketManagerApi.Mapper.TicketMapper;

public static class TicketSummaryMapper
{
  public static TicketSummaryDTO ToDTO(this Ticket ticket)
  {
    return new TicketSummaryDTO
    {
      Id = ticket.Id,
      CreatedAt = ticket.CreatedAt,
      UpdatedAt = ticket.UpdatedAt,
      Title = ticket.Title,
      Severity = ticket.Severity,
      Status = ticket.Status
    };
  }
}
