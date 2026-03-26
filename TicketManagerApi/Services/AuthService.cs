using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicketManagerApi.Data;
using TicketManagerApi.DTO.UsersDTO;
using TicketManagerApi.Entities;
using TicketManagerApi.Mapper.UserMapper;
using BC = BCrypt.Net.BCrypt;

namespace TicketManagerApi.Services;

public class AuthService (
    TicketManagerContext dbContext,
    IValidator<UserRegisterDto> userRegisterDtoValidator
)
{
  public async Task<UserSummaryDto> Register(
    UserRegisterDto userRegisterDto
  )
  {
    // Check if user already exists
    await userRegisterDtoValidator.ValidateAndThrowAsync(userRegisterDto);
    // Save user to database
    var userRole = await dbContext.Roles
                          .Where(r => r.RoleName == RoleName.USER)
                          .ToListAsync();

    User newUser = new()
    {
      Username = userRegisterDto.Username is null ? userRegisterDto.Email : userRegisterDto.Username,
      Email = userRegisterDto.Email,
      Password = HashPassword(userRegisterDto.Password),
      Roles = userRole
    };
    dbContext.Users.Add(newUser);
    await dbContext.SaveChangesAsync();

    return UserMapper.ToUserSummaryDto(newUser);
  }

  public string HashPassword(string password)
  {
    string hash = BC.HashPassword(password, 11);
    return hash;
  }
}
