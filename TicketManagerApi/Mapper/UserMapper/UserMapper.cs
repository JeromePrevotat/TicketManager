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
      Username = user.Username is null ? user.Email : user.Username,
      Email = user.Email
    };
  }
}
