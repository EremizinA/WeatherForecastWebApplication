using System.Reflection;
using WeatherForecast.Api.Controllers;
using WeatherForecast.Api.Data;
using WeatherForecast.Api.Logging;
using WeatherForecast.Api.Options;
using WeatherForecast.Api.Repositories;
using WeatherForecast.Api.Services;

var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets(Assembly.GetExecutingAssembly());

var config = configBuilder.Build();

var host = WebApplication.CreateBuilder(args);

// Add services to the container.

host.Services.AddRazorPages();

host.Services.Configure<DbConnectionOptions>(config.GetSection(nameof(DbConnectionOptions)));

host.Services.AddSingleton<ISqlServerDbConnectionFactory, SqlServerDbConnectionFactory>();

host.Services.AddSingleton<IWeatherTodayRepository, WeatherTodayRepository>();
host.Services.AddSingleton<IWeatherTodayService, WeatherTodayService>();
host.Services.AddSingleton<ICountryRepository, CountryRepository>();
host.Services.AddSingleton<ICountryService, CountryService>();
host.Services.AddSingleton<ICityRepository, CityRepository>();
host.Services.AddSingleton<ICityService, CityService>();
host.Services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
host.Services.AddSingleton<WeatherTodayController, WeatherTodayController>();
host.Services.AddSingleton<CountryController, CountryController>();
host.Services.AddSingleton<CityController, CityController>();

var app = host.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
