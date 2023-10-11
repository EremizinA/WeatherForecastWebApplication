using WeatherForecast.Api.Models;

namespace WeatherForecast.Api.Services
{
    public interface IWeatherTodayService
    {
        Task<IEnumerable<WeatherToday>> GetAllAsync();
        Task<bool> CreateAllAsync(IEnumerable<WeatherToday> weatherTodayList);
        Task<bool> DeleteAllAsync();
    }
}
