using MassTransit;

namespace MottuAnalytics.Modules.TinyUrlEvent.Consumers.Starter
{
    public static class TinyUrlEventConsumerStarter
    {
        public static void Start(IBusRegistrationConfigurator bussConfigurator)
        {
            bussConfigurator.AddConsumer<TinyUrlCreatedConsumer>();
            bussConfigurator.AddConsumer<TinyUrlViewedConsumer>();
            bussConfigurator.AddConsumer<GetTinyUrlAnalyticsConsumer>();
        }
    }
}
