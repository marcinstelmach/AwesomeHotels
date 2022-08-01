using AwesomeHotels.Services.Users.Domain.Entities;
using AwesomeHotels.Services.Users.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace AwesomeHotels.Services.Users.Infrastructure;

public class UsersDbContext : DbContext
{
    public UsersDbContext(DbContextOptions<UsersDbContext> options)
    : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
    }
}