using KafkaFlow;
using Microsoft.OpenApi.Models;
using Spents.ReadModel.API.Extensions;
using Spents.ReadModel.Crosscutting.Extensions;
using Spents.ReadModel.Crosscutting.Middlewares;
using Spents.ReadModel.Crosscutting.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hosting, config) =>
{
    var currentDirectory = Directory.GetCurrentDirectory();
    config
        .SetBasePath(currentDirectory)
        .AddJsonFile($"{currentDirectory}/appsettings.json");
});

var applicationSettings = builder.Configuration.GetSection("Settings").Get<Settings>();
// Add services to the container.

builder.Services
    .AddKafka(applicationSettings.KafkaSettings)
    .AddRepositories()
    .AddDependecyInjection()
    .AddLoggingDependency()
    .AddMiddlewares()
    .AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Spents ReadModel", Version = "v1" });
    c.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, "Spents.ReadModel.xml"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();
