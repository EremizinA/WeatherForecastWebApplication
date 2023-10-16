using WeatherForecast.Api.Models.Database;
using WeatherForecast.Api.Models.Service;

namespace WeatherForecast.Api.Mappers
{
    public static class WeatherTodayMapper
    {
        public static WeatherTodayFrontEnd Map(this WeatherTodayFrontEndDto source)
        {
            return new WeatherTodayFrontEnd
            {
                CityName = source.CityName,
                Temperature = source.Temperature,
                Scale = source.Scale,
                CountryName = source.CountryName,
            };
        }
    }
}
