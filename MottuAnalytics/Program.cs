using MottuAnalytics.Database;
using MottuAnalytics.Modules.TinyUrlEvent;
using MottuAnalytics.Modules.TinyUrlEvent.Consumers.Starter;
using MottuShared.Cache;
using MottuShared.Cache.Options;
using MottuShared.Messaging.MassTransit.RabbitMQ;
using MottuShared.Swagger;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Instanciate configuration object to use in static startups methods
var configuration = builder.Configuration;
var services = builder.Services;

//Initiate caching
CacheStartup.Config(services, configuration, new CacheStartupOptions() { UseMemoryCache = true });

//Database startup configuration
DatabaseStartup.Config(services, configuration);

// Initiate the modules
TinyUrlEventModuleStartup.Config(services);

//Initiate Swagger
SwaggerStartup.Config(services);

//Initiate MassTransit + RabbitMQ
MassTransitMQStartup.Config(services, configuration, TinyUrlEventConsumerStarter.Start);

// Add services to the container.
builder.Services.AddControllers();

//Setting up Serilog
builder.Host.UseSerilog((ctx, cfg) =>
    cfg.ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
