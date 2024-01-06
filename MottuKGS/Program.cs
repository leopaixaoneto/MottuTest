using MottuKGS.BackgroundJobs;
using MottuKGS.Database;
using MottuKGS.Modules.GeneratedKey;
using MottuKGS.Modules.GeneratedKey.Consumers.Starter;
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
GeneratedKeyModuleStartup.Config(services);

//Initiate Swagger
SwaggerStartup.Config(services);

//Initiate MassTransit + RabbitMQ
MassTransitMQStartup.Config(services, configuration, GeneratedKeyEventConsumerStarter.Start);

//Initialize Quartz (BackgroundJobs)
BackgroundJobsStartup.Config(services);

// Add services to the container.
builder.Services.AddControllers();

//Setting up Serilog
builder.Host.UseSerilog((ctx, cfg) =>
    cfg.ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

if (!app.Environment.IsDevelopment()) 
    app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
