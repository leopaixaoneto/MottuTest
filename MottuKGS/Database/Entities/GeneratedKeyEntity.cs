namespace MottuKGS.Database.Entities
{
    public class GeneratedKeyEntity
    {
        public Guid Id { get; set; }
        public string Key { get; set; } = String.Empty;
        public bool IsUsed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
