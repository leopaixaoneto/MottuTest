namespace MottuShared.Contracts.Responses
{
    public record GetGeneratedKeyResponse
    {
        public List<string> GeneratedKeys { get; set; } = new();
    }
}
