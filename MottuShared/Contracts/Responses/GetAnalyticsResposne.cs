namespace MottuShared.Contracts.Requests
{
    public record GetAnalyticsResponse
    {
        public Guid Id { get; set; }
        public long Views { get; set; }
        public DateTime LastViewedAt { get; set; }
    }
}
