namespace AwesomeHotels.Services.Users.Domain.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}