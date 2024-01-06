using Microsoft.EntityFrameworkCore;
using MottuKGS.Database.Entities;
using MottuKGS.Database.EntityBuilder;

namespace MottuKGS.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<GeneratedKeyEntity> GeneratedKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity(GeneratedKeyEntityBuilder.build);
        }
    }
}
