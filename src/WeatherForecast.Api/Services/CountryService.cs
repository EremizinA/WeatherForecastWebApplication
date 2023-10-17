using System.Diagnostics;
using WeatherForecast.Api.Logging;
using WeatherForecast.Api.Models.Service;
using WeatherForecast.Api.Repositories;

namespace WeatherForecast.Api.Services
{
    public class CountryService: ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ILoggerAdapter<CountryService> _logger;

        public CountryService(ICountryRepository weatherForecastRepository,
            ILoggerAdapter<CountryService> logger)
        {
            _countryRepository = weatherForecastRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Country?>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all countries");
            var stopWatch = Stopwatch.StartNew();
            try
            {
                return await _countryRepository.GetAllAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong while retrieving all countries");
                throw;
            }
            finally
            {
                stopWatch.Stop();
                _logger.LogInformation("All countries retrieved in {0}ms", stopWatch.ElapsedMilliseconds);
            }
        }

        public async Task<Country?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving country with id: {0}", id);
            var stopWatch = Stopwatch.StartNew();
            try
            {
                return await _countryRepository.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong while retrieving country with id {0}", id);
                throw;
            }
            finally
            {
                stopWatch.Stop();
                _logger.LogInformation("Country with id {0} retrieved in {1}ms", id, stopWatch.ElapsedMilliseconds);
            }
        }

        public async Task<bool> CreateAsync(Country? country)
        {
            _logger.LogInformation("Creating country with name: {0}", country?.Name);
            var stopWatch = Stopwatch.StartNew();
            try
            {
                return await _countryRepository.CreateAsync(country);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong while creating a country");
                throw;
            }
            finally
            {
                stopWatch.Stop();
                _logger.LogInformation("Country with name {0} created in {1}ms", country?.Name, stopWatch.ElapsedMilliseconds);
            }
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            _logger.LogInformation("Deleting country with id: {0}", id);
            var stopWatch = Stopwatch.StartNew();
            try
            {
                return await _countryRepository.DeleteByIdAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong while deleting country with id {0}", id);
                throw;
            }
            finally
            {
                stopWatch.Stop();
                _logger.LogInformation("Country with country {0} deleted in {1}ms", id, stopWatch.ElapsedMilliseconds);
            }
        }
    }
}
