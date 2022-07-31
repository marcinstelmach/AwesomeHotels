using System.Text.RegularExpressions;
using AwesomeHotels.Services.Users.Domain.Exceptions;
using BuildingBlocks.Domain;

namespace AwesomeHotels.Services.Users.Domain.ValueObjects;

public class EmailAddress : ValueObject
{
    private const string EmailRegexPattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

    private EmailAddress(string value)
    {
        Value = value;
        CheckInvariants();
    }

    public string Value { get; private set; }

    public static EmailAddress Create(string email) => new(email);

    private void CheckInvariants()
    {
        if (string.IsNullOrWhiteSpace(Value))
        {
            throw new InvalidEmailException("EmailAddress cannot be null or empty", Value);
        }

        var isEmail = Regex.IsMatch(Value, EmailRegexPattern, RegexOptions.IgnoreCase);
        if (!isEmail)
        {
            throw new InvalidEmailException($"Given email is not correct email address", Value);
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}