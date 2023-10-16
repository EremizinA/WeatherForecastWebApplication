using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Api.Controllers;
using WeatherForecast.Api.Logging;
using WeatherForecast.Api.Models.Service;
using WeatherForecast.Api.Repositories;
using WeatherForecast.Api.Services;
using WeatherForecast.Tests.Common.Data;

namespace WeatherForecast.Tests.Unit
{
    public class WeatherTodayServiceTests: UnitTestBase
    {
        private WeatherTodayService _weatherTodayService;
        private readonly IWeatherTodayRepository _weatherTodayRepository = Substitute.For<IWeatherTodayRepository>();
        private readonly ILoggerAdapter<WeatherTodayService> _logger = Substitute.For<ILoggerAdapter<WeatherTodayService>>();
        private Random _random;

        [SetUp]
        public override void Setup()
        {
            _weatherTodayService = new WeatherTodayService(_weatherTodayRepository, _logger);
            _random = new Random();
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnWeatherForToday_WhenWeatherForTodayExists()
        {
            //Arrange
            var expectedWeatherForToday = WeatherTodayGenerator.GenerateRandomWeatherToday(_random.Next(10));
            _weatherTodayRepository.GetAllAsync().Returns(expectedWeatherForToday);

            //Act
            var actualWeatherForToday = await _weatherTodayService.GetAllAsync();

            //Assert
            actualWeatherForToday.Should().BeEquivalentTo(expectedWeatherForToday);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenWeatherForTodayDoesNotExist()
        {
            //Arrange
            _weatherTodayRepository.GetAllAsync().Returns(Enumerable.Empty<WeatherToday>());

            //Act
            var actualWeatherForToday = await _weatherTodayService.GetAllAsync();

            //Assert
            actualWeatherForToday.Should().BeEmpty();
        }

        [Test]
        public async Task GetAllAsync_ShouldLogMessages_WhenInvoked()
        {
            // Arrange
            _weatherTodayRepository.GetAllAsync().Returns(Enumerable.Empty<WeatherToday>());

            // Act
            await _weatherTodayService.GetAllAsync();

            // Assert
            _logger.Received(1).LogInformation(Arg.Is("Retrieving all weather data"));
            _logger.Received(1).LogInformation(Arg.Is("All weather data retrieved in {0}ms"), Arg.Any<long>());
        }
    }
}
