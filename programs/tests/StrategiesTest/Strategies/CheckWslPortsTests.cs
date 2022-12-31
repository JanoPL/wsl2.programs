using HelperTest;
using Strategies;
using Strategies.Strategy;

namespace StrategiesTest.Strategies
{
    public class CheckWslPortsTests
    {
        private CheckWslPorts SetupService()
        {
            var wsl = new WslHelper();
            var logger = wsl.GetLogger();

            return new CheckWslPorts(logger, wsl.GetIWsl(wsl.GetSettings()));
        }

        [Fact]
        public void CheckWslPortsTest()
        {
            Assert.IsAssignableFrom<IStrategies>(SetupService());
        }

        [Fact]
        public void ExecuteTest()
        {
            StrategiesTestHelper.ExecuteMethod(SetupService());
        }
    }
}