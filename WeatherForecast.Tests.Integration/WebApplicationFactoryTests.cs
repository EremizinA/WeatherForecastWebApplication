using WeatherForecast.Tests.Common.Extentions;
using FluentAssertions;
using WeatherForecast.Api.Models;
using static WeatherForecast.Tests.Integration.SetUpFixture;
using static WeatherForecast.Tests.Integration.IntegrationTestsData;
using WeatherForecast.Tests.Common.Helpers;
using NUnit.Framework;

namespace WeatherForecast.Tests.Integration
{
    [TestFixture]
    public class WebApplicationFactoryTests
    {
        //CRUD tests: Create, Read, Update, Delete
        [Test]
        public async Task Get_AllWeatherForToday_WeatherShouldBeRetrieved()
        {
            //Act
            var response = await WeatherForecastTestClient
                .GetAsync<List<WeatherToday>>("WeatherToday/GetWeatherForToday");

            //Assert
            response.Should().NotBeNull();
            response.Count.Should().BeGreaterThan(0);
        }

        [TestCaseSource(typeof(IntegrationTestsData), nameof(GetWeatherTodayDataSource))]
        public async Task Create_WeatherForToday_WeatherShouldBeCreated(IEnumerable<WeatherToday> weatherForToday)
        {
            //Arrange
            const string Uri = "WeatherToday/CreateWeatherForToday";
            var weatherContent = HttpHelper.ConvertToStringHttpContent(weatherForToday);

            //Act
            var response = await WeatherForecastTestClient
                .PostAsync<WeatherIdsList>(Uri, weatherContent);
            
            //Assert
            var dataFromTable = await MsSqlClient.Select<WeatherToday>("Select * from WeatherToday");
            var dataFromTableList = dataFromTable.ToList();
            Assert.Multiple(() =>
            {
                response.Should().NotBeNull();
                response.WeatherIds.Should().ContainAll(weatherForToday.Select(w => w.Id.ToString()).ToList());
            });
        }
    }
}
