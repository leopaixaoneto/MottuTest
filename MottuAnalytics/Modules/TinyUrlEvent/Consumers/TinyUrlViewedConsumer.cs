using MassTransit;
using MottuAnalytics.Modules.TinyUrlEvent.DTO;
using MottuAnalytics.Modules.TinyUrlEvent.Enums;
using MottuAnalytics.Modules.TinyUrlEvent.Services;
using MottuShared.Contracts.Events;

namespace MottuAnalytics.Modules.TinyUrlEvent.Consumers
{
    public class TinyUrlViewedConsumer : IConsumer<TinyUrlViewedEvent>
    {
        private readonly CreateTinyUrlEventAction _createTinyUrlEventUseCase;

        public TinyUrlViewedConsumer(CreateTinyUrlEventAction createTinyUrlEventUseCase)
        {
            _createTinyUrlEventUseCase = createTinyUrlEventUseCase;
        }

        public async Task Consume(ConsumeContext<TinyUrlViewedEvent> context)
        {
            try
            {
                await _createTinyUrlEventUseCase.Execute(new CreateTinyUrlEventRequest()
                {
                    TinyUrlId = context.Message.Id,
                    CreatedAt = context.Message.ViewedAt,
                    EventType = EventType.Viewed
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
