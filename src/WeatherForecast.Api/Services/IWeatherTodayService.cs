using WeatherForecast.Api.Models.Service;

namespace WeatherForecast.Api.Services
{
    public interface IWeatherTodayService
    {
        Task<IEnumerable<WeatherToday>> GetAllAsync();
        Task<IEnumerable<WeatherTodayFrontEnd>> GetAllFrontEndAsync();
        Task<bool> CreateAllAsync(IEnumerable<WeatherToday> weatherTodayList);
        Task<bool> DeleteAllAsync();
    }
}
