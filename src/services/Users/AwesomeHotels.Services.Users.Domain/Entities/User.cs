using AwesomeHotels.Services.Users.Domain.Exceptions;
using AwesomeHotels.Services.Users.Domain.ValueObjects;
using BuildingBlocks.Domain;

namespace AwesomeHotels.Services.Users.Domain.Entities;

public class User : IAggregateRoot
{
    public User(UserId id, EmailAddress emailAddress, string firstName, string lastName, DateOfBirth dateOfBirth, Password password, DateTimeOffset creationDateTime)
    {
        Id = id;
        EmailAddress = emailAddress;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Password = password;
        CreationDateTime = creationDateTime;
        IsDeleted = false;

        CheckInvariants();
    }

    public UserId Id { get; private set; }

    public EmailAddress EmailAddress { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public DateOfBirth DateOfBirth { get; private set; }

    public Password Password { get; private set; }

    public DateTimeOffset CreationDateTime { get; private set; }

    public bool IsDeleted { get; private set; }

    public static User Create(long id, string email, string firstName, string lastName, DateOnly dateOfBirth, string passwordHash, string securityStamp)
    {
        var userId = UserId.Create(id);
        var emailAddress = EmailAddress.Create(email);
        var date = DateOfBirth.Create(dateOfBirth);
        var password = Password.Create(passwordHash, securityStamp);

        return new User(userId, emailAddress, firstName, lastName, date, password, DateTimeOffset.UtcNow);
    }

    private void CheckInvariants()
    {
        if (string.IsNullOrWhiteSpace(FirstName))
        {
            throw new InvalidUserException("Null or empty User FirstName");
        }

        if (string.IsNullOrWhiteSpace(LastName))
        {
            throw new InvalidUserException("Null or empty User LastName");
        }
    }
}