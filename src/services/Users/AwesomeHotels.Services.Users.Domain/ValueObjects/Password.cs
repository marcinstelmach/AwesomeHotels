using AwesomeHotels.Services.Users.Domain.Exceptions;
using BuildingBlocks.Domain;

namespace AwesomeHotels.Services.Users.Domain.ValueObjects;

public class Password : ValueObject
{
    protected Password(string passwordHash, string securityStamp)
    {
        PasswordHash = passwordHash;
        SecurityStamp = securityStamp;
    }

    public string PasswordHash { get; set; }

    public string SecurityStamp { get; set; }

    public static Password Create(string passwordHash, string securityStamp) => new(passwordHash, securityStamp);

    private void CheckInvariants()
    {
        if (string.IsNullOrWhiteSpace(PasswordHash))
        {
            throw new InvalidUserPasswordException("PasswordHash cannot be null or whitespace");
        }

        if (string.IsNullOrWhiteSpace(SecurityStamp))
        {
            throw new InvalidUserPasswordException("SecurityStamp cannot be null or whitespace");
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return PasswordHash;
        yield return SecurityStamp;
    }
}