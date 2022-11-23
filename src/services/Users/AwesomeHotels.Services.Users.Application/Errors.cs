using ErrorOr;

namespace AwesomeHotels.Services.Users.Application;

public static class Errors
{
    public static class AddUser
    {
        public static Error UserAlreadyExists(string email) => Error.Conflict("User.WithSameEmailExists", $"User with given email {email} address already exists.");
    }

}