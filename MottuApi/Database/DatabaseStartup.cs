using Microsoft.EntityFrameworkCore;
using MottuTest.Database.Options;

namespace MottuTest.Database
{
    public static class DatabaseStartup
    {
        public static void Config(IServiceCollection service, ConfigurationManager config)
        {
            // Add database configuration
            service.ConfigureOptions<DatabaseOptionsSetup>();

            // Add Database context into services
            service.AddDbContext<DatabaseContext>(
                o => o.UseNpgsql(config.GetConnectionString("database")));
        }
    }
}
