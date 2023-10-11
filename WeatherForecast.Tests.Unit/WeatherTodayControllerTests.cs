using WeatherForecast.Api.Controllers;
using WeatherForecast.Api.Services;
using WeatherForecast.Api.Models;
using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using WeatherForecast.Tests.Common.Data;

namespace WeatherForecast.Tests.Unit
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public class WeatherTodayControllerTests
    {
        private WeatherTodayController _weatherTodayController;
        private readonly IWeatherTodayService _weatherTodayService = Substitute.For<IWeatherTodayService>();
        private Random _random;

        [SetUp]
        public void Setup()
        {
            _weatherTodayController = new WeatherTodayController(_weatherTodayService);
            _random = new Random();
        }

        [Test]
        public async Task GetAllWeather_ReturnOkAndObject_WhenWeatherForTodayExists()
        {
            //Arrange
            var weatherToday = WeatherTodayGenerator.GenerateRandomWeatherToday(_random.Next(10));
            _weatherTodayService.GetAllAsync().Returns(weatherToday);

            //Act
            var result = (OkObjectResult)await _weatherTodayController.GetAll();

            //Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(weatherToday);
        }

        [Test]
        public async Task GetAllWeather_ShouldReturnEmptyList_WhenNoWeatherTodayExist()
        {
            //Arrange
            _weatherTodayService.GetAllAsync().Returns(Enumerable.Empty<WeatherToday>());

            //Act
            var result = (OkObjectResult)await _weatherTodayController.GetAll();

            //Assert
            result.StatusCode.Should().Be(200);
            result.Value.As<IEnumerable<WeatherToday>>().Should().BeEmpty();
        }

        [Test]
        public async Task CreateAllWeather_ReturnOkAmdObject_WhenWeatherWasCreated()
        {
            //Arrange
            var weatherToday = WeatherTodayGenerator.GenerateRandomWeatherToday(_random.Next(10));
            _weatherTodayService.CreateAllAsync(weatherToday).Returns(true);

            //Act
            var result = (CreatedAtActionResult)await _weatherTodayController.CreateAll(weatherToday);

            //Assert
            result.StatusCode.Should().Be(201);
            result.Value.Should().BeEquivalentTo(new { WeatherIds = string.Join(" ", weatherToday.Select(x => x.Id)) });
        }

        [Test]
        public async Task CreateAllWeather_ReturnOkAndObject_WhenWeatherWasCreated()
        {
            //Arrange
            var weatherToday = WeatherTodayGenerator.GenerateRandomWeatherToday(_random.Next(10));
            _weatherTodayService.CreateAllAsync(weatherToday).Returns(true);

            //Act
            var result = (CreatedAtActionResult)await _weatherTodayController.CreateAll(weatherToday);

            //Assert
            result.StatusCode.Should().Be(201);
            result.Value.Should()
                .BeEquivalentTo(new { WeatherIds = string.Join(" ", weatherToday.Select(x => x.Id)) });
        }

        [Test]
        public async Task CreateAllWeather_ReturnBadRequest_WhenWeatherWasNotCreated()
        {
            //Arrange
             _weatherTodayService.CreateAllAsync(Arg.Any<IEnumerable<WeatherToday>>()).Returns(false);

            //Act
            var result = (BadRequestResult)await _weatherTodayController
                .CreateAll(new List<WeatherToday> { new WeatherToday()});

            //Assert
            result.StatusCode.Should().Be(400);
        }

        [Test]
        public async Task DeleteAllWeather_ReturnOk_WhenWeatherWasDeleted()
        {
            //Arrange
            _weatherTodayService.DeleteAllAsync().Returns(true);

            //Act
            var result = (OkResult) await _weatherTodayController.DeleteAll();

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task DeleteAllWeather_ReturnNotFound_WhenWeatherWasNotDeleted()
        {
            //Arrange
            _weatherTodayService.DeleteAllAsync().Returns(false);

            //Act
            var result = (NotFoundResult)await _weatherTodayController.DeleteAll();

            //Assert
            result.StatusCode.Should().Be(404);
        }
    }
}