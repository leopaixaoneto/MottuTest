namespace MottuTest.Modules.TinyURL.DTO
{
    public record CreateTinyUrlRequest
    {
        public string Url { get; set; } = string.Empty;
    }
}
