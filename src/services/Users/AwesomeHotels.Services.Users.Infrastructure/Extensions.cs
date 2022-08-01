using AwesomeHotels.Services.Users.Application;
using BuildingBlocks.Application;
using BuildingBlocks.Infrastructure.Bus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeHotels.Services.Users.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMediatR(UsersApplicationAssembly.Assembly);
        services.AddScoped<IBus, MediatRBus>();

        return services;
    }
}