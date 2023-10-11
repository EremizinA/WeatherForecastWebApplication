using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Api.Options;

namespace WeatherForecast.Tests.Integration.CustomWebApplicationFactory
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public IConfigurationRoot? Configuration { get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                // remove the existing context configuration
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbConnectionOptions));
                if (descriptor != null)
                    services.Remove(descriptor);

                Configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.test.json", optional: false, reloadOnChange: true)
                    .AddUserSecrets(Assembly.GetExecutingAssembly())
                    .Build();

                services.Configure<DbConnectionOptions>(Configuration.GetSection(nameof(DbConnectionOptions)));
            });
        }
    }
}
