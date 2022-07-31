using AwesomeHotels.Services.Users.Domain.Exceptions;
using BuildingBlocks.Domain;

namespace AwesomeHotels.Services.Users.Domain.ValueObjects;

public class UserId : ValueObject
{
    private UserId(long id)
    {
        Id = id;
        CheckInvariants();
    }

    public long Id { get; private set; }

    public static UserId Create(long id) => new(id);

    private void CheckInvariants()
    {
        if (Id < 1)
        {
            throw new InvalidUserIdException(Id);
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}