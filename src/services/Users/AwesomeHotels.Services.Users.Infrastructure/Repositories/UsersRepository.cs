using AwesomeHotels.Services.Users.Domain.Entities;
using AwesomeHotels.Services.Users.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AwesomeHotels.Services.Users.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly UsersDbContext _usersDbContext;

    public UsersRepository(UsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _usersDbContext.Users.ToArrayAsync();
    }
}