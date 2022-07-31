using AwesomeHotels.Services.Users.Domain.Exceptions;
using BuildingBlocks.Domain;

namespace AwesomeHotels.Services.Users.Domain.ValueObjects;

public class DateOfBirth : ValueObject
{
    private DateOfBirth(DateOnly date)
    {
        Date = date;
        CheckInvariants();
    }

    public DateOnly Date { get; private set; }

    public static DateOfBirth Create(DateOnly dateOnly) => new(dateOnly);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Date;
    }

    private void CheckInvariants()
    {
        if (Date == default)
        {
            throw new InvalidDateOfBirthValueException("Default value is not correct for DateOfBirth", Date);
        }

        if (Date > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new InvalidDateOfBirthValueException("Date of birth cannot be in feature", Date);
        }

        var difference = DateTime.UtcNow.Subtract(Date.ToDateTime(TimeOnly.MinValue));
        if (difference > TimeSpan.FromDays(365 * 200))
        {
            throw new InvalidDateOfBirthValueException("People does not live over 200 years", Date);
        }
    }
}