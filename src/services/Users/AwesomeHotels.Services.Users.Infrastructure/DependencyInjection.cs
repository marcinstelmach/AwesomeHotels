using AwesomeHotels.Services.Users.Application;
using AwesomeHotels.Services.Users.Domain.Factories;
using AwesomeHotels.Services.Users.Domain.Repositories;
using AwesomeHotels.Services.Users.Infrastructure.Repositories;
using AwesomeHotels.Services.Users.Infrastructure.Validation;
using BuildingBlocks.Application;
using BuildingBlocks.Application.Bus;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeHotels.Services.Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMediatR(UsersApplicationAssemblyInfo.Assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddScoped<IBus, MediatRBus>();
        services.AddSnowflakeIdGenerator(new Random().Next(1, 200));

        using var provider = services.BuildServiceProvider();
        var configuration = provider.GetRequiredService<IConfiguration>();
        var dbConnectionString = configuration["DbConnectionString"];

        services.AddDbContext<UsersDbContext>(x => x.UseSqlServer(dbConnectionString));
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IUnitOfWork, UsersRepository>();

        services.AddTransient<IPasswordHasher<string>, PasswordHasher<string>>();
        services.AddTransient<IPasswordFactory, PasswordFactory>();

        return services;
    }
}