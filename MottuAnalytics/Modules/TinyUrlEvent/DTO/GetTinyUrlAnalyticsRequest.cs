namespace MottuAnalytics.Modules.TinyUrlEvent.DTO
{
    public record GetTinyUrlAnalyticsRequest
    {
        public Guid TinyUrlId { get; set; }
    }
}
