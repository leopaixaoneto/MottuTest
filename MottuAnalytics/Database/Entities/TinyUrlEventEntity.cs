using MottuAnalytics.Modules.TinyUrlEvent.Enums;

namespace MottuAnalytics.Database.Entities
{
    public class TinyUrlEventEntity
    {
        public Guid Id { get; set; }
        public Guid TinyUrlId { get; set; }
        public EventType Type { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
