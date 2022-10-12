using Mapster;
using MapsterMapper;

namespace AwesomeHotels.Services.Users.Api.Mappings;

public static class Extensions
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(UsersApiAssemblyInfo.Assembly);
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}