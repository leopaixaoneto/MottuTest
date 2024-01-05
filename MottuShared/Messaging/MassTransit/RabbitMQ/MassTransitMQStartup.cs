
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace MottuShared.Messaging.MassTransit.RabbitMQ
{
    public static class MassTransitMQStartup
    {
        public static void Config(IServiceCollection service, ConfigurationManager config, Action<IBusRegistrationConfigurator>? consumersStarter = null)
        {
            service.AddMassTransit(massConfig =>
            {
                massConfig.SetKebabCaseEndpointNameFormatter();

                if(consumersStarter is not null) 
                    consumersStarter(massConfig);

                massConfig.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(new Uri(config["MessageBroker:Host"]!), h =>
                    {
                        h.Username(config["MessageBroker:Username"]);
                        h.Password(config["MessageBroker:Password"]);
                    });

                    configurator.ConfigureEndpoints(context);
                });
            });

        }
    }
}
