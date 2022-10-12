using AwesomeHotels.Services.Users.Domain.Exceptions;
using BuildingBlocks.Domain;

namespace AwesomeHotels.Services.Users.Domain.ValueObjects;

public class UserId : ValueObject
{
    private UserId(long value)
    {
        Value = value;
        CheckInvariants();
    }

    public long Value { get; private set; }

    public static UserId Create(long id) => new(id);

    private void CheckInvariants()
    {
        if (Value < 1)
        {
            throw new InvalidUserIdException(Value);
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}