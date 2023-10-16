using WeatherForecast.Api.Models.Service;

namespace WeatherForecast.Api.Services
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllAsync();
        Task<Country?> GetByIdAsync(int id);
        Task<bool> CreateAsync(Country country);
        Task<bool> DeleteByIdAsync(int id);
    }
}
