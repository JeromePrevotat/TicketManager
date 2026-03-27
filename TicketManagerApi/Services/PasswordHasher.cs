using Microsoft.AspNetCore.Identity;
using TicketManagerApi.Entities;
using BC = BCrypt.Net.BCrypt;
namespace TicketManagerApi.Services;

public class PasswordHasher : IPasswordHasher<User>
{
  public string HashPassword(User user, string password)
  {
    return BC.EnhancedHashPassword(password, 11);    
  }

  public PasswordVerificationResult VerifyHashedPassword(
    User user,
    string hashedPassword,
    string providedPassword
  )
  {
    return BC.EnhancedVerify(providedPassword, hashedPassword) 
      ? PasswordVerificationResult.Success
      : PasswordVerificationResult.Failed;
  }
}
