using MottuKGS.Modules.GeneratedKey.Repository.Implementation;
using MottuKGS.Modules.GeneratedKey.Services;

namespace MottuKGS.Modules.GeneratedKey
{
    public static class GeneratedKeyModuleStartup
    {
        public static void Config(IServiceCollection service)
        {
            //Repositories
            service.AddScoped<GeneratedKeyRepository>();

            //Actions
            service.AddScoped<GenerateShortenedCodeAction>();
            service.AddScoped<GetCachedGeneratedKeyAction>();
            service.AddScoped<GenerateBatchOfNewKeysAction>();
        }
    }
}
