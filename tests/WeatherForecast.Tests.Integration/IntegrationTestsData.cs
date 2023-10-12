using WeatherForecast.Api.Models;
using WeatherForecast.Tests.Common.Data;

namespace WeatherForecast.Tests.Integration
{
    public static class IntegrationTestsData
    {
        public static IEnumerable<IEnumerable<WeatherToday>> GetWeatherTodayDataSource()
        {
            yield return WeatherTodayGenerator.GenerateRandomWeatherToday(new Random().Next(10)).ToList();
        }
    }
}
