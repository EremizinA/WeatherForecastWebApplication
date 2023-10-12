using WeatherForecast.Api.Models;

namespace WeatherForecast.Api.Repositories
{
    public interface IWeatherTodayRepository
    {
        Task<IEnumerable<WeatherToday>> GetAllAsync();
        Task<bool> CreateAllAsync(IEnumerable<WeatherToday> weatherTodayList);
        Task<bool> DeleteAllAsync();
    }
}
