using WeatherForecast.Api.Logging;
using WeatherForecast.Api.Repositories;
using WeatherForecast.Api.Services;
using WeatherForecast.Api.Options;
using WeatherForecast.Api.Data;
using WeatherForecast.Api;
using Microsoft.AspNetCore;

public class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();

}




