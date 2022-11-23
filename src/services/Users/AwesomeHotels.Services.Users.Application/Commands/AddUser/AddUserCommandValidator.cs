using FluentValidation;

namespace AwesomeHotels.Services.Users.Application.Commands.AddUser;

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    private const int MinAge = 18;

    public AddUserCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MinimumLength(3);
        RuleFor(x => x.LastName).NotEmpty().MinimumLength(3);
        RuleFor(x => x.DateOfBirth).LessThan(DateOnly.FromDateTime(DateTime.Now).AddYears(-MinAge));
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().Equal(x => x.ConfirmPassword);
    }
}