using MassTransit;

namespace Infrastructure.MassTransit;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddMassTransit(builder =>
        {
            if (environment.IsDevelopment())
            {
                builder.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h => {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ConfigureEndpoints(context);
                });
            }
            else // this shows an example of how you may use a different AMQP service for production.
            {
                builder.UsingAzureServiceBus((context,cfg) =>
                {
                    cfg.Host("your connection string");
                    cfg.ConfigureEndpoints(context);
                });
            }
        });

        // // MassTransit also has a Mediator implementation built in
        // services.AddMediator(builder =>
        // {
        //     builder.AddConsumer<MyConsumer>();
        // });
        
        return services;
    }
}