using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Api.Models;

namespace WeatherForecast.Tests.Common.Data
{
    public static class WeatherTodayGenerator
    {
        private static readonly Random _random = new ();

        public static IEnumerable<WeatherToday> GenerateRandomWeatherToday(int count = 1)
        {
            var weatherTodayList = new List<WeatherToday>();

            for(int i = 0; i < count; i++)
            {
                var weather = new WeatherToday()
                {
                    CityId = _random.Next(1, 10),
                    Scale = _random.Next(2) % 2 == 0 ? "°C" : "°F"
                };
                weather.Temperature = weather.Scale.Equals("°C") ? _random.Next(40) : _random.Next(50, 100);

                weatherTodayList.Add(weather);
            }

            return weatherTodayList;
        }
    }
}
