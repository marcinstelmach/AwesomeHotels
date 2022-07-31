using BuildingBlocks.Domain;

namespace AwesomeHotels.Services.Users.Domain.Exceptions;

public class InvalidDateOfBirthValueException : DomainException
{
    public InvalidDateOfBirthValueException(string message, DateOnly value)
        : base(message)
    {
        Value = value;
    }

    public DateOnly Value { get; set; }
}