using AwesomeHotels.Services.Users.Domain.Entities;
using BuildingBlocks.Domain.Specifications;

namespace AwesomeHotels.Services.Users.Domain.Repositories;

public interface IUsersRepository
{
    Task<IEnumerable<User>> GetUsersAsync(Specification<User> specification);

    Task<bool> ExistsAsync(Specification<User> specification);

    void Add(User user);
}