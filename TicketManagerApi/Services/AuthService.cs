using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicketManagerApi.Data;
using TicketManagerApi.DTO.UsersDTO;
using TicketManagerApi.Entities;
using TicketManagerApi.Mapper.UserMapper;
using TicketManagerApi.Validators;
using BC = BCrypt.Net.BCrypt;

namespace TicketManagerApi.Services;

public class AuthService (
    TicketManagerContext dbContext,
    IValidator<UserRegisterDto> userRegisterDtoValidator,
    IValidator<UserLoginDto> userLoginDtoValidator
)
{
  public async Task<UserSummaryDto> Register(UserRegisterDto userRegisterDto)
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

  public async Task<UserSummaryDto?> Login(UserLoginDto userLoginDto){
    await userLoginDtoValidator.ValidateAndThrowAsync(userLoginDto);

    var query = dbContext.Users.AsQueryable();
    if (!string.IsNullOrEmpty(userLoginDto.Username)){
      query = query.Where(u => u.Username == userLoginDto.Username);
    }
    else {
      query = query.Where(u => u.Email == userLoginDto.Email);
    }
    var user = await query.FirstOrDefaultAsync();

    if (user is null || !BC.EnhancedVerify(userLoginDto.Password, user.Password))
    {
      return null;
    }

    return UserMapper.ToUserSummaryDto(user);
  }

  private static string HashPassword(string password)
  {
    string hash = BC.EnhancedHashPassword(password, 11);
    return hash;
  }
}
