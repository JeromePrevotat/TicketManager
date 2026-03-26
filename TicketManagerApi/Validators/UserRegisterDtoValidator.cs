using FluentValidation;
using TicketManagerApi.Data;
using TicketManagerApi.DTO.UsersDTO;
using Microsoft.EntityFrameworkCore;

namespace TicketManagerApi.Validators;

public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
{
  public UserRegisterDtoValidator(TicketManagerContext dbContext)
  {
    RuleFor(x => x.Email)
      .NotEmpty()
      .EmailAddress()
      .MustAsync(async (email, cancellation) => 
        !await dbContext.Users.AnyAsync(u => u.Email == email, cancellation))
      .WithMessage("Email is already taken.");

    RuleFor(x => x.Password)
      .NotEmpty()
      .MinimumLength(6);

    RuleFor(x => x.Username)
      .MinimumLength(3)
      .MustAsync(async (username, cancellation) => 
        !await dbContext.Users.AnyAsync(u => u.Username == username, cancellation))
      .WithMessage("Username is already taken.");
  }
}
