using MottuTest.Modules.TinyURL.Services;
using MottuTest.Modules.TinyURL.Repository.Interfaces;
using MottuTest.Modules.TinyURL.Repository.Implementation;
using MottuApi.Modules.TinyURL.Services;

namespace MottuTest.Modules.TinyURL
{
    public static class TinyUrlModuleStartup
    {
        public static void Config(IServiceCollection service)
        {
            //Repositories
            service.AddScoped<TinyUrlRepository>();
            service.AddScoped<ITinyUrlRepository, CachedTinyUrlRepository>();

            //Actions
            service.AddScoped<CreateTinyUrlAction>();
            service.AddScoped<GetTinyUrlAction>();
            service.AddScoped<GetTinyUrlInfoAction>();
            service.AddScoped<GetTinyUrlAnalyticsAction>();
            service.AddScoped<GetGeneratedKeyAction>();
        }
    }
}
