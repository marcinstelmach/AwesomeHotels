using BuildingBlocks.Domain;

namespace AwesomeHotels.Services.Users.Domain.Exceptions;

public class InvalidUserException : DomainException
{
    public InvalidUserException(string message)
        : base(message)
    {
    }
}