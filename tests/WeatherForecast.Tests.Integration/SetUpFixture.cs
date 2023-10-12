using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Reflection;
using WeatherForecast.Api.Data;
using WeatherForecast.Api.Options;
using WeatherForecast.Tests.Common.Services;
using WeatherForecast.Tests.Common.Services.WeatherForecastTestApp;
using NUnit.Framework;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using WeatherForecast.Tests.Integration.CustomWebApplicationFactory;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WeatherForecast.Tests.Integration
{
    [SetUpFixture]
    public class SetUpFixture
    {
        public static MsSqlClient MsSqlClient { get; set; }

        public static HttpClient WeatherForecastTestClient { get; set; }

        [OneTimeSetUp] 
        public async Task OneTimeSetUp() 
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json", optional: false, reloadOnChange: true)
                .AddUserSecrets(Assembly.GetExecutingAssembly());
  
            var sqlConnectionFactory = new SqlServerDbConnectionFactory(Options
                .Create(configurationBuilder.Build().GetSection(nameof(DbConnectionOptions)).Get<DbConnectionOptions>()));

            MsSqlClient = new MsSqlClient(sqlConnectionFactory);

            WeatherForecastTestClient = new CustomWebApplicationFactory<Program>().CreateClient();

            //WeatherForecastTestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //WeatherForecastTestClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            //await MsSqlClient.ExecuteSqlScript(File.ReadAllText("Data/Scripts/CreateDbAndTables.sql"));
        }

        [OneTimeTearDown] 
        public void OneTimeTearDown() 
        {
            WeatherForecastTestClient.Dispose();
        }
    }
}
