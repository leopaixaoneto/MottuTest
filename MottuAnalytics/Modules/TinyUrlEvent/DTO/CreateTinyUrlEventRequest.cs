using MottuAnalytics.Modules.TinyUrlEvent.Enums;

namespace MottuAnalytics.Modules.TinyUrlEvent.DTO
{
    public record CreateTinyUrlEventRequest
    {
        public Guid TinyUrlId { get; set; }
        public DateTime CreatedAt { get; set; }
        public EventType EventType { get; set; }
    }
}
