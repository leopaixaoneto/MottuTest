using MottuTest.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MottuTest.Database.EntityBuilder;

public static class TinyUrlEntityBuilder
{
    public static readonly Action<EntityTypeBuilder<TinyUrlEntity>> build = builder =>
    {
        builder.ToTable("Tinyurl");
        builder.Property(t => t.ShortenedCode).HasMaxLength(7);
        builder.HasIndex(t => t.ShortenedCode).IsUnique();
    };
}

