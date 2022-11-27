using AwesomeHotels.Services.Users.Domain.Exceptions;
using AwesomeHotels.Services.Users.Domain.Factories;
using BuildingBlocks.Domain;

namespace AwesomeHotels.Services.Users.Domain.ValueObjects;

public class Password : ValueObject
{
    private Password(string hash)
    {
        Hash = hash;
        CheckInvariants();
    }

    public string Hash { get; }

    public static Password Create(IPasswordFactory factory, string password)
    {
        var hash = factory.CreatePassword(password);
        return new Password(hash);
    }

    private void CheckInvariants()
    {
        if (string.IsNullOrWhiteSpace(Hash))
        {
            throw new InvalidUserPasswordException("Hash cannot be null or whitespace");
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Hash;
    }
}