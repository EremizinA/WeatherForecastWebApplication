using WeatherForecast.Api.Models.Database;
using WeatherForecast.Api.Models.Service;

namespace WeatherForecast.Api.Repositories
{
    public interface IWeatherTodayRepository
    {
        Task<IEnumerable<WeatherToday>> GetAllAsync();
        Task<IEnumerable<WeatherTodayFrontEndDto>> GetAllFrontEndDtoAsync();
        Task<bool> CreateAllAsync(IEnumerable<WeatherToday> weatherTodayList);
        Task<bool> DeleteAllAsync();
    }
}
