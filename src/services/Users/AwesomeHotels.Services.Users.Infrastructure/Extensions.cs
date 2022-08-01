﻿using AwesomeHotels.Services.Users.Application;
using AwesomeHotels.Services.Users.Domain.Repositories;
using AwesomeHotels.Services.Users.Infrastructure.Repositories;
using BuildingBlocks.Application;
using BuildingBlocks.Application.Bus;
using BuildingBlocks.Infrastructure.Bus;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeHotels.Services.Users.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMediatR(UsersApplicationAssembly.Assembly);
        services.AddScoped<IBus, MediatRBus>();
        services.AddSnowflakeIdGenerator(new Random().Next(1, 200));

        using var provider = services.BuildServiceProvider();
        var configuration = provider.GetRequiredService<IConfiguration>();
        var dbConnectionString = configuration["DbConnectionString"];

        services.AddDbContext<UsersDbContext>(x => x.UseSqlServer(dbConnectionString));
        services.AddScoped<IUsersRepository, UsersRepository>();

        return services;
    }
}