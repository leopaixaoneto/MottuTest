namespace MottuTest.Database.Entities
{
    public class TinyUrlEntity
    {
        public Guid Id { get; set; }
        public string Original { get; set; } = string.Empty;
        public string Shortened { get; set; } = string.Empty;
        public string ShortenedCode { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
    }
}
