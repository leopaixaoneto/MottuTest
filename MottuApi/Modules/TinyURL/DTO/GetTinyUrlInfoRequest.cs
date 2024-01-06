namespace MottuApi.Modules.TinyURL.DTO
{
    public record GetTinyUrlInfoRequest
    {
        public Guid Id { get; set; }
    }
}
