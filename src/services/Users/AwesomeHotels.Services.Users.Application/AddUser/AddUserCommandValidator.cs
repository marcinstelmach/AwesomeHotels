using FluentValidation;

namespace AwesomeHotels.Services.Users.Application.AddUser;

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.FirstName).MinimumLength(3);
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.DateOfBirth).LessThan(DateOnly.FromDateTime(DateTime.Now).AddYears(-18));
    }
}