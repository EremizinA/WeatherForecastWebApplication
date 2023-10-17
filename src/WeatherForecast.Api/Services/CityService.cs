using System.Diagnostics;
using WeatherForecast.Api.Logging;
using WeatherForecast.Api.Models.Service;
using WeatherForecast.Api.Repositories;

namespace WeatherForecast.Api.Services
{
    public class CityService: ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly ILoggerAdapter<CityService> _logger;

        public CityService(ICityRepository cityRepository,
            ILoggerAdapter<CityService> logger)
        {
            _cityRepository = cityRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<City?>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all cities");
            var stopWatch = Stopwatch.StartNew();
            try
            {
                return await _cityRepository.GetAllAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong while retrieving all cities");
                throw;
            }
            finally
            {
                stopWatch.Stop();
                _logger.LogInformation("All cities retrieved in {0}ms", stopWatch.ElapsedMilliseconds);
            }
        }

        public async Task<City?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving city with id: {0}", id);
            var stopWatch = Stopwatch.StartNew();
            try
            {
                return await _cityRepository.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong while retrieving city with id {0}", id);
                throw;
            }
            finally
            {
                stopWatch.Stop();
                _logger.LogInformation("City with id {0} retrieved in {1}ms", id, stopWatch.ElapsedMilliseconds);
            }
        }

        public async Task<IEnumerable<City?>> GetAllByCountryNameAsync(string countryName)
        {
            _logger.LogInformation($"Retrieving cities from {countryName}");
            var stopWatch = Stopwatch.StartNew();
            try
            {
                return await _cityRepository.GetAllByCountryNameAsync(countryName);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong while retrieving cities for {countryName}");
                throw;
            }
            finally
            {
                stopWatch.Stop();
                _logger.LogInformation($"Cities for country {countryName} retrieved in {0}ms", stopWatch.ElapsedMilliseconds);
            }
        }

        public async Task<bool> CreateAsync(City? city)
        {
            _logger.LogInformation("Creating city with name: {0}", city?.Name);
            var stopWatch = Stopwatch.StartNew();
            try
            {
                return await _cityRepository.CreateAsync(city);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong while creating a city");
                throw;
            }
            finally
            {
                stopWatch.Stop();
                _logger.LogInformation("City with name {0} created in {1}ms", city?.Name, stopWatch.ElapsedMilliseconds);
            }
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            _logger.LogInformation("Deleting city with id: {0}", id);
            var stopWatch = Stopwatch.StartNew();
            try
            {
                return await _cityRepository.DeleteByIdAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong while deleting city with id {0}", id);
                throw;
            }
            finally
            {
                stopWatch.Stop();
                _logger.LogInformation("City with country {0} deleted in {1}ms", id, stopWatch.ElapsedMilliseconds);
            }
        }
    }
}
