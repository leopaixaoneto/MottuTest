using Serilog;
using MottuTest.Database;
using MottuTest.Modules.TinyURL;

using MottuShared.Swagger;
using MottuShared.Cache;
using MottuShared.Cache.Options;
using MassTransit;
using MottuShared.Messaging.MassTransit.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

//Instanciate configuration object to use in static startups methods
var configuration = builder.Configuration;
var services = builder.Services;

//Initiate caching
CacheStartup.Config(services, configuration, new CacheStartupOptions() { UseRedisCache = true});

//Database startup configuration
DatabaseStartup.Config(services, configuration);

// Initiate the modules
TinyUrlModuleStartup.Config(services);

//Initiate Swagger
SwaggerStartup.Config(services);

//Initiate MassTransit + RabbitMQ
MassTransitMQStartup.Config(services, configuration);

// Add services to the container.
builder.Services.AddControllers();

//Setting up Serilog
builder.Host.UseSerilog((ctx, cfg) =>
    cfg.ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{ 
    app.UseSwagger(); 
    app.UseSwaggerUI();
}else{
    app.UseHttpsRedirection();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
