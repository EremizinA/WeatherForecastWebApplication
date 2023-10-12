using NUnit.Framework;

namespace WeatherForecast.Tests.Unit
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public class UnitTestBase
    {
        [SetUp]
        public virtual void Setup()
        {

        }

        [TearDown]
        public virtual void TearDown()
        {

        }
    }
}
