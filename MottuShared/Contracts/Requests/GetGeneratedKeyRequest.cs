namespace MottuShared.Contracts.Requests
{
    public record GetGeneratedKeyRequest
    {
        public int NumberOfKeys { get; set; }
    }
}
