using System.Diagnostics;
using WeatherForecast.Api.Logging;
using WeatherForecast.Api.Mappers;
using WeatherForecast.Api.Models.Database;
using WeatherForecast.Api.Models.Service;
using WeatherForecast.Api.Repositories;

namespace WeatherForecast.Api.Services
{
    public class WeatherTodayService: IWeatherTodayService
    {
        private readonly IWeatherTodayRepository _weatherTodayRepository;
        private readonly ILoggerAdapter<WeatherTodayService> _logger;

        public WeatherTodayService(IWeatherTodayRepository weatherTodayRepository,
            ILoggerAdapter<WeatherTodayService> logger)
        {
            _weatherTodayRepository = weatherTodayRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<WeatherToday>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all weather data");
            var stopWatch = Stopwatch.StartNew();
            try
            {
                return await _weatherTodayRepository.GetAllAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong while retrieving all weather data");
                throw;
            }
            finally
            {
                stopWatch.Stop();
                _logger.LogInformation("All weather data retrieved in {0}ms", stopWatch.ElapsedMilliseconds);
            }
        }

        public async Task<IEnumerable<WeatherTodayFrontEnd>> GetAllFrontEndAsync()
        {
            _logger.LogInformation("Retrieving all weather data for UI");
            var stopWatch = Stopwatch.StartNew();
            try
            {
                var weatherTodayDto = await _weatherTodayRepository.GetAllFrontEndDtoAsync();
                return weatherTodayDto.Select(dto => dto.Map());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong while retrieving all weather data for UI");
                throw;
            }
            finally
            {
                stopWatch.Stop();
                _logger.LogInformation("All weather data for UI retrieved in {0}ms", stopWatch.ElapsedMilliseconds);
            }
        }

        public async Task<bool> CreateAllAsync(IEnumerable<WeatherToday> weatherTodayList)
        {
            _logger.LogInformation($"Creating weather for today with date (UTC): {DateTime.UtcNow}");
            var stopWatch = Stopwatch.StartNew();
            try
            {
                return await _weatherTodayRepository.CreateAllAsync(weatherTodayList);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong while creating a weather for today");
                throw;
            }
            finally
            {
                stopWatch.Stop();
                _logger.LogInformation($"Weather for today with date (UTC) {DateTime.UtcNow} created in {stopWatch.ElapsedMilliseconds}ms");
            }
        }

        public async Task<bool> DeleteAllAsync()
        {
            _logger.LogInformation($"Deleting weather for today with date (UTC): {DateTime.UtcNow}");
            var stopWatch = Stopwatch.StartNew();
            try
            {
                return await _weatherTodayRepository.DeleteAllAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong while deleting a weather for today");
                throw;
            }
            finally
            {
                stopWatch.Stop();
                _logger.LogInformation($"Weather for today with date (UTC) {DateTime.UtcNow} was deleted in {stopWatch.ElapsedMilliseconds}ms");
            }
        }
    }
}
