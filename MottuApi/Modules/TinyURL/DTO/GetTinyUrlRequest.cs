namespace MottuTest.Modules.TinyURL.DTO
{
    public record GetTinyUrlRequest
    {
        public string ShortenedCode { get; set; } = String.Empty;
    }
}
