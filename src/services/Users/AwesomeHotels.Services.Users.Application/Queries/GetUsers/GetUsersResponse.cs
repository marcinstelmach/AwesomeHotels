namespace AwesomeHotels.Services.Users.Application.Queries.GetUsers;

public record GetUsersResponse(long Id, string Email, string FirstName, string LastName, DateOnly DateOfBirth, DateTimeOffset CreationDateTime);