using AwesomeHotels.Services.Users.Domain.Entities;

namespace AwesomeHotels.Services.Users.Domain.Repositories;

public interface IUsersRepository
{
    Task<IEnumerable<User>> GetUsersAsync();
}