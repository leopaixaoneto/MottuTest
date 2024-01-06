using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MottuKGS.Database.Entities;
using MottuKGS.Modules.GeneratedKey.Services;

namespace MottuKGS.Database.EntityBuilder
{
    public static class GeneratedKeyEntityBuilder
    {
        public static readonly Action<EntityTypeBuilder<GeneratedKeyEntity>> build = builder =>
        {
            builder.ToTable("Generatedkey");
            builder.Property(t => t.Key).HasMaxLength(GenerateShortenedCodeAction.ShortenedLinkCharsCount);
            builder.HasIndex(t => t.Key).IsUnique();
        };
    }
}
