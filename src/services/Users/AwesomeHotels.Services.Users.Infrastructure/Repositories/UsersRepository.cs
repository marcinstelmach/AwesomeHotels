using AwesomeHotels.Services.Users.Domain.Entities;
using AwesomeHotels.Services.Users.Domain.Repositories;
using BuildingBlocks.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace AwesomeHotels.Services.Users.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository, IUnitOfWork
{
    private readonly UsersDbContext _usersDbContext;

    public UsersRepository(UsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }

    public async Task<IEnumerable<User>> GetUsersAsync(Specification<User> specification)
    {
        return await _usersDbContext.Users.Where(specification.ToExpression()).ToArrayAsync();
    }

    public async Task<bool> ExistsAsync(Specification<User> specification)
    {
        return await _usersDbContext.Users.AnyAsync(specification.ToExpression());
    }

    public void Add(User user)
    {
        _usersDbContext.Users.Add(user);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _usersDbContext.SaveChangesAsync();
    }
}