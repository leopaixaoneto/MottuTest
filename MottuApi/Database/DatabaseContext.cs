using Microsoft.EntityFrameworkCore;
using MottuTest.Database.Entities;
using MottuTest.Database.EntityBuilder;

namespace MottuTest.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<TinyUrlEntity> TinyUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity(TinyUrlEntityBuilder.build);
        }
    }
}
