using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using NLog;
using NLog.Web;
using VideoHosting.API.Mapping;
using VideoHosting.Core.Interfaces;
using VideoHosting.Infrastructure;
using VideoHosting.Infrastructure.Interfaces;
using VideoHosting.Infrastructure.Repositories;
using VideoHosting.Infrastructure.Utility;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Add configuration services
    builder.Services.AddMediatR(cfg =>
    {
        var assembly = AppDomain.CurrentDomain.Load("VideoHosting.Core");
        cfg.RegisterServicesFromAssembly(assembly);
    });
    builder.Services.AddSingleton<CosmosDbInitializer>();
    builder.Services.AddScoped<CosmosDbContext>();
    builder.Services.Configure<CosmosDbSettings>(builder.Configuration.GetSection("CosmosDbSettings"));
    builder.Services.AddScoped<IVideoFileRepository, VideoFileRepository>();
    builder.Services.AddScoped<IVideoRepository, VideoRepository>();
    builder.Services.AddScoped<ICommentRepository, CommentRepository>();
    builder.Services.AddSingleton<IConfigurationParser, ConfigurationParser>();
    builder.Services.Configure<IISServerOptions>(options =>
    {
        options.MaxRequestBodySize = int.MaxValue;
    });
    builder.Services.Configure<KestrelServerOptions>(options =>
    {
        options.Limits.MaxRequestBodySize = int.MaxValue;
    });
    builder.Services.Configure<FormOptions>(options =>
    {
        options.MultipartBodyLengthLimit = int.MaxValue;
    });
    builder.Services.Configure<IISServerOptions>(options =>
    {
        options.MaxRequestBodySize = int.MaxValue;
    });
    builder.Services.AddAutoMapper(typeof(MappingProfile));
    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    await app.Services.GetRequiredService<CosmosDbInitializer>().InitializeAsync();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    // NLog: catch setup errors
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}