using FluentValidation;
using TicketManagerApi.DTO.UsersDTO;

namespace TicketManagerApi.Validators;

public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
{
  public UserLoginDtoValidator()
  {
    RuleFor(x => x.Username)
      .NotEmpty()
      .When(x => string.IsNullOrEmpty(x.Email))
      .WithMessage("Either username or email must be provided.");

    RuleFor(x => x.Email)
      .NotEmpty()
      .EmailAddress()
      .When(x => string.IsNullOrEmpty(x.Username))
      .WithMessage("Either email or username must be provided.");

    RuleFor(x => x.Password)
      .NotEmpty();
  }
}
