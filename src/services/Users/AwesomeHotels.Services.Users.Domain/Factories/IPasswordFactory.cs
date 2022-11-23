namespace AwesomeHotels.Services.Users.Domain.Factories;

public interface IPasswordFactory
{
    string CreatePassword(string password);
}