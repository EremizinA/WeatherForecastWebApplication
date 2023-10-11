using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using WeatherForecast.Api.Options;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherForecast.Tests.Common.Services.WeatherForecastTestApp
{
    public class WeatherForecastTestApp<TProgram> : WebApplicationFactory<TProgram>
        where TProgram : class
    {
        public IConfigurationRoot Configuration { get; set; }

        public WeatherForecastTestApp() 
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json", optional: false, reloadOnChange: true)
                .AddUserSecrets(Assembly.GetExecutingAssembly());

            Configuration = builder.Build();
        }


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbConnectionOptionsService = services.SingleOrDefault(s => s.ServiceType == typeof(DbConnectionOptions)) ?? default;

                services.Remove(dbConnectionOptionsService);
                
                services.Configure<DbConnectionOptions>(Configuration.GetSection("DbConnectionOptions"));
            });
        }


    }
}
