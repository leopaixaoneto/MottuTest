namespace MottuShared.Contracts.Events
{
    public record TinyUrlViewedEvent
    {
        public Guid Id { get; set; }

        public DateTime ViewedAt { get; set; }
    }

}



