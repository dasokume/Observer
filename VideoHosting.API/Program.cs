using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using System.Reflection;
using System.Security.Claims;
using VideoHosting.API.Auth;
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

    var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = domain;
        options.Audience = builder.Configuration["Auth0:Audience"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.NameIdentifier
        };
    });
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("read:video", policy => policy.Requirements.Add(new HasScopeRequirement("read:video", domain)));
        options.AddPolicy("write:video", policy => policy.Requirements.Add(new HasScopeRequirement("write:video", domain)));
        options.AddPolicy("write:comment", policy => policy.Requirements.Add(new HasScopeRequirement("write:comment", domain)));
        options.AddPolicy("read:comment", policy => policy.Requirements.Add(new HasScopeRequirement("read:comment", domain)));
    });

    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "API Documentation",
            Version = "v1.0",
            Description = ""
        });
        options.ResolveConflictingActions(x => x.First());
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            BearerFormat = "JWT",
            Flows = new OpenApiOAuthFlows
            {
                Implicit = new OpenApiOAuthFlow
                {
                    TokenUrl = new Uri($"https://{builder.Configuration["Auth0:Domain"]}/oauth/token"),
                    AuthorizationUrl = new Uri($"https://{builder.Configuration["Auth0:Domain"]}/authorize?audience={builder.Configuration["Auth0:Audience"]}"),
                    Scopes = new Dictionary<string, string>
                    {
                        { "openid", "OpenId" },
                    }
                },
                
            }
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                },
                new[] { "openid" }
            }
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    });

    // Add configuration services
    builder.Services.AddMediatR(cfg =>
    {
        var assembly = AppDomain.CurrentDomain.Load("VideoHosting.Core");
        cfg.RegisterServicesFromAssembly(assembly);
    });

    // AddSingleton
    builder.Services.AddSingleton<CosmosDbInitializer>();
    builder.Services.AddSingleton<IConfigurationParser, ConfigurationParser>();
    builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

    // AddScoped
    builder.Services.AddScoped<IVideoFileRepository, VideoFileRepository>();
    builder.Services.AddScoped<IVideoRepository, VideoRepository>();
    builder.Services.AddScoped<ICommentRepository, CommentRepository>();
    builder.Services.AddScoped<CosmosDbContext>();
    
    // Config
    builder.Services.Configure<CosmosDbSettings>(builder.Configuration.GetSection("CosmosDbSettings"));
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
        app.UseSwaggerUI(settings =>
        {
            settings.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1.0");
            settings.OAuthClientId(builder.Configuration["Auth0:ClientId"]);
            settings.OAuthClientSecret(builder.Configuration["Auth0:ClientSecret"]);
            settings.OAuthUsePkce();
        });
    }

    await app.Services.GetRequiredService<CosmosDbInitializer>().InitializeAsync();

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthentication();
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