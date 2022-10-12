namespace AwesomeHotels.Services.Users.Api.Controllers.Users.v1;

public record AddUserRequest(
    string Email,
    string FirstName,
    string LastName,
    string Password,
    string ConfirmPassword,
    DateTime DateOfBirth);
