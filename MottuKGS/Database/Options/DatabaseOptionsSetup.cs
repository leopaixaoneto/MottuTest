using Microsoft.Extensions.Options;

namespace MottuKGS.Database.Options
{
    public class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
    {
        private const string ConfigurationSectionName = "DatabaseOptions";
        private readonly IConfiguration _configuration;

        public DatabaseOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(DatabaseOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            var connString = _configuration.GetConnectionString("DatabaseOptions");

            options.ConnectionString = connString ?? string.Empty;

            _configuration.GetSection(ConfigurationSectionName).Bind(options);
        }
    }
}
