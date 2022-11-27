using BuildingBlocks.Domain;

namespace AwesomeHotels.Services.Users.Domain.Exceptions;

public class InvalidUserIdException : DomainException
{
    public InvalidUserIdException(long id)
        : base($"Given value '{id}' is invalid for UserId")
    {
    }
}