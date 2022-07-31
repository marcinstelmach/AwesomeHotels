using BuildingBlocks.Domain;

namespace AwesomeHotels.Services.Users.Domain.Exceptions;

public class InvalidUserPasswordException : DomainException
{
    public InvalidUserPasswordException(string message)
        : base(message)
    {
    }
}