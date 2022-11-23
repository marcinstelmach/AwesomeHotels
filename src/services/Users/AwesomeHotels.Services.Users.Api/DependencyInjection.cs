using AwesomeHotels.Services.Users.Api.Mappings;
using AwesomeHotels.Services.Users.Api.Utilities;

namespace AwesomeHotels.Services.Users.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IProblemFactory, ProblemFactory>();
        services.AddMappings();

        return services;
    }
}