using MassTransit;
using MottuAnalytics.Modules.TinyUrlEvent.DTO;
using MottuAnalytics.Modules.TinyUrlEvent.Enums;
using MottuAnalytics.Modules.TinyUrlEvent.Services;
using MottuShared.Contracts.Events;

namespace MottuAnalytics.Modules.TinyUrlEvent.Consumers
{
    public class TinyUrlCreatedConsumer : IConsumer<TinyUrlCreatedEvent>
    {
        private readonly CreateTinyUrlEventAction _createTinyUrlEventUseCase;

        public TinyUrlCreatedConsumer(CreateTinyUrlEventAction createTinyUrlEventUseCase)
        {
            _createTinyUrlEventUseCase = createTinyUrlEventUseCase;
        }

        public async Task Consume(ConsumeContext<TinyUrlCreatedEvent> context)
        {
            try
            {
                await _createTinyUrlEventUseCase.Execute(new CreateTinyUrlEventRequest()
                {
                    TinyUrlId = context.Message.Id,
                    CreatedAt = context.Message.CreatedAt,
                    EventType = EventType.Created
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
