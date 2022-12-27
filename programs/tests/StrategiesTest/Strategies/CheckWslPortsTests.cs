using HelperTest;
using Strategies;
using Strategies.Strategy;

namespace StrategiesTest.Strategies
{
    public class CheckWslPortsTests
    {
        [Fact()]
        public void CheckWslPortsTest()
        {
            var wsl = new WslHelper();
            var logger = wsl.GetLogger();

            CheckWslPorts checkWslPorts = new(logger, wsl.GetIWsl(wsl.GetSettings()));

            Assert.IsAssignableFrom<IStrategies>(checkWslPorts);
        }

        [Fact()]
        public void ExecuteTest()
        {
            var wsl = new WslHelper();
            var logger = wsl.GetLogger();

            CheckWslPorts checkWslPorts = new(logger, wsl.GetIWsl(wsl.GetSettings()));

            try {
                checkWslPorts.Execute();
            } catch (Exception ex) {
                Assert.Fail(ex.Message);
            }

            Assert.True(true);
        }
    }
}