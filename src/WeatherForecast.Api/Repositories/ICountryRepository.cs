using WeatherForecast.Api.Models.Service;

namespace WeatherForecast.Api.Repositories;

public interface ICountryRepository
{
    Task<IEnumerable<Country>> GetAllAsync();
    Task<Country?> GetByIdAsync(int id);
    Task<bool> CreateAsync(Country country);
    Task<bool> DeleteByIdAsync(int id);
}
