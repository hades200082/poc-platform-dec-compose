using Infrastructure.Cosmos;
using Infrastructure.MassTransit;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddOptions();
        services.AddCosmos(configuration, environment);
        services.AddMassTransit(configuration, environment);
        
        return services;
    }
}