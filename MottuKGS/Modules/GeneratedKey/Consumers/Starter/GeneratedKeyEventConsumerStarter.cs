using MassTransit;

namespace MottuKGS.Modules.GeneratedKey.Consumers.Starter
{
    public static class GeneratedKeyEventConsumerStarter
    {
        public static void Start(IBusRegistrationConfigurator bussConfigurator)
        {
            bussConfigurator.AddConsumer<GetGeneratedKeyConsumer>();
        }
    }
}
