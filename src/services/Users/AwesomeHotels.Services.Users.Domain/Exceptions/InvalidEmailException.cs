using BuildingBlocks.Domain;

namespace AwesomeHotels.Services.Users.Domain.Exceptions;

public class InvalidEmailException : DomainException
{
    public string Email { get; }

    public InvalidEmailException(string message, string email)
        : base(message)
    {
        Email = email;
    }
}