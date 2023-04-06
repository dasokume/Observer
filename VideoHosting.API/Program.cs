using VideoHosting.Core.Interfaces;
using VideoHosting.Infrastructure;

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
builder.Services.AddScoped<IVideoRepository, CosmosDbVideoRepository>();
builder.Services.AddSingleton<CosmosDbInitializer>();
builder.Services.AddSingleton<CosmosDbContext>();
builder.Services.Configure<CosmosDbSettings>(builder.Configuration.GetSection("CosmosDbSettings"));

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