using Domain.Abstractions;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Options;

namespace Infrastructure.Cosmos;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCosmos(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services.Configure<CosmosOptions>(configuration.GetSection(nameof(CosmosOptions)));

        services.AddHttpClient<CosmosClient>()
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler();
                
                if (environment.IsDevelopment())
                {
                    /*
                     If we're running in "development" then it means we are running locally. As such it is assumed
                     that we're running with the Cosmos DB Emulator in Docker, using the provided docker-compose.yml
                     in the git root.
                     
                     Since the cosmos emulator uses a self-signed certificate, and it running in Docker means it can't
                     "trust" itself in the host environment, we tell the application to accept any certificate when
                     running in the development environment.
                    */
                    handler.ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                }

                return handler;
            });

        services.AddSingleton<CosmosClient>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<CosmosOptions>>().Value;
            var httpClient = provider.GetRequiredService<IHttpClientFactory>().CreateClient(nameof(CosmosClient));

            var builder = new CosmosClientBuilder(options.ConnectionString)
                .WithSerializerOptions(new CosmosSerializationOptions
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                })
                .WithHttpClientFactory(() => httpClient);

            if (environment.IsDevelopment())
            {
                builder.WithConnectionModeGateway(); // Must set gateway mode for running in docker.
            }

            return builder.Build();
        });

        services.AddSingleton<ICosmosService, CosmosService>();

        // Adds all repositories for all entities
        services.AddSingleton(typeof(IRepository<>), typeof(CosmosRepository<>));

        return services;
    }
}