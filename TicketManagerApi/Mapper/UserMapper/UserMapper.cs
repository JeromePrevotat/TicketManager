using TicketManagerApi.DTO.UsersDTO;
using TicketManagerApi.Entities;

namespace TicketManagerApi.Mapper.UserMapper;

public static class UserMapper
{
  public static UserSummaryDto ToUserSummaryDto(User user)
  {
    return new UserSummaryDto
    {
      Id = user.Id,
      Username = user.UserName ?? user.Email!,
      Email = user.Email!
    };
  }
}
