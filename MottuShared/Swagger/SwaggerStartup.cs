using Microsoft.Extensions.DependencyInjection;

namespace MottuShared.Swagger
{
    public static class SwaggerStartup
    {
        public static void Config(IServiceCollection service)
        {
            //Initiate swagger into services
            service.AddEndpointsApiExplorer();
            service.AddSwaggerGen();
        }
    }
}
