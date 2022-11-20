using BuildingBlocks.Application.Bus;
using ErrorOr;

namespace AwesomeHotels.Services.Users.Application.Commands.AddUser;

public record AddUserCommand(
    string Email,
    string FirstName,
    string LastName,
    string Password,
    string ConfirmPassword,
    DateOnly DateOfBirth)
    : ICommand<ErrorOr<long>>;