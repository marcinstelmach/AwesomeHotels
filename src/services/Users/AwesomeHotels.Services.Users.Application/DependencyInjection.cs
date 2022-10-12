using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeHotels.Services.Users.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(UsersApplicationAssemblyInfo.Assembly);
        return services;
    }
}