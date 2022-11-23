using AwesomeHotels.Services.Users.Domain.Factories;
using Microsoft.AspNetCore.Identity;

namespace AwesomeHotels.Services.Users.Infrastructure;

public class PasswordFactory : IPasswordFactory
{
    private readonly IPasswordHasher<string> _passwordHasher;

    public PasswordFactory(IPasswordHasher<string> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }


    public string CreatePassword(string password)
    {
        return _passwordHasher.HashPassword(string.Empty, password);
    }
}