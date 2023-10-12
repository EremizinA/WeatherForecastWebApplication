using NUnit.Framework;

namespace WeatherForecast.Tests.Integration
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestBase
    {
        [SetUp]
        public virtual void Setup()
        {

        }

        [TearDown]
        public virtual void Teardown()
        {

        }
    }
}
