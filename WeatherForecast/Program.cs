using Microsoft.Extensions.Options;
using rush01.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Set configuration from file
builder.Configuration.AddJsonFile("appsettings.json");

// set data to ServiceSettings object from configuration
builder.Services.Configure<ServiceSettings>(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//DI - WeatherClient dependency is registered 
builder.Services.AddScoped<IWeatherClient, WeatherClient>();

builder.Services.AddMemoryCache();

builder.Services.AddSwaggerGen(
    options =>
    {
        var filename = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
        var filepath = Path.Combine(AppContext.BaseDirectory, filename);
        options.IncludeXmlComments(filepath);
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Map("/", (IOptions<ServiceSettings> options) => 
{
    ServiceSettings service = options.Value;  // получаем переданные через Options объект ServiceSettings
    return service.ApiKey;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
