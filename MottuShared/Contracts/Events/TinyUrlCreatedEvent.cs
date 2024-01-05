namespace MottuShared.Contracts.Events
{
    public record TinyUrlCreatedEvent
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}



