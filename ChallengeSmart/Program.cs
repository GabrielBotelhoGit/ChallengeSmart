using ChallengeSmart.Configs;
using ChallengeSmart.Interfaces;
using ChallengeSmart.Services;
using FluentValidation.AspNetCore;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddFluentValidation(s =>
    {
        s.RegisterValidatorsFromAssemblyContaining<Program>();
    })
    .AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())); ;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfiguration configuration = builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

AirPortConfig airPortConfig = configuration.GetSection(nameof(AirPortConfig)).Get<AirPortConfig>();
builder.Services.AddSingleton(airPortConfig);
builder.Services.AddSingleton<IAirPortService, AirPortService>();

builder.Services.AddHttpClient(nameof(AirPortService), clientConfig =>
{
    clientConfig.BaseAddress = new Uri(airPortConfig.BaseUrl);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
