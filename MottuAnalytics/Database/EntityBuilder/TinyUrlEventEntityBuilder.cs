using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MottuAnalytics.Database.Entities;

namespace MottuAnalytics.Database.EntityBuilder
{
    public static class TinyUrlEventEntityBuilder
    {
        public static readonly Action<EntityTypeBuilder<TinyUrlEventEntity>> build = builder =>
        {
            builder.ToTable("Tinyurlevent");
            builder.HasIndex(t => t.Id).IsUnique();
        };
    }
}
