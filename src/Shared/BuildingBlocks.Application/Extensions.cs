using BuildingBlocks.Application.IdGeneration;
using IdGen.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Application;

public static class Extensions
{
    public static IServiceCollection AddSnowflakeIdGenerator(this IServiceCollection services, int number)
    {
        services.AddIdGen(number);
        services.AddSingleton<IIdGenerator, SnowflakeIdGenerator>();
        return services;
    }
}