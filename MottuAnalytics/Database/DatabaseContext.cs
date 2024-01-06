using Microsoft.EntityFrameworkCore;
using MottuAnalytics.Database.Entities;
using MottuAnalytics.Database.EntityBuilder;

namespace MottuAnalytics.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<TinyUrlEventEntity> TinyUrlEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity(TinyUrlEventEntityBuilder.build);
        }
    }
}
