using MottuAnalytics.Modules.TinyUrlEvent.Repository.Implementation;
using MottuAnalytics.Modules.TinyUrlEvent.Repository.Interfaces;
using MottuAnalytics.Modules.TinyUrlEvent.Services;

namespace MottuAnalytics.Modules.TinyUrlEvent
{
    public static class TinyUrlEventModuleStartup
    {
        public static void Config(IServiceCollection service)
        {
            //Repositories
            service.AddScoped<TinyUrlEventRepository>();
            service.AddScoped<ITinyUrlEventRepository, CachedTinyUrlEventRepository>();

            //Actions
            service.AddScoped<CreateTinyUrlEventAction>();
            service.AddScoped<GetTinyUrlAnalyticsAction>();
        }
    }
}
