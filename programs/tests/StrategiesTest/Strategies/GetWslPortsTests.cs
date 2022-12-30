using HelperTest;
using Strategies;
using Strategies.Strategy;

namespace StrategiesTest.Strategies
{
    public class GetWslPortsTests
    {
        [Fact()]
        public void GetWslPortsTest()
        {
            var wsl = new WslHelper();
            var logger = wsl.GetLogger();
            var ports = wsl.GetPortsList();

            GetWslPorts checkWslIPAddress = new(logger, ports);

            Assert.IsAssignableFrom<IStrategies>(checkWslIPAddress);
        }

        [Fact()]
        public void ExecuteTest()
        {
            var wsl = new WslHelper();
            var logger = wsl.GetLogger();
            var ports = wsl.GetPortsList();

            GetWslPorts checkWslIPAddress = new(logger, ports);

            try {
                checkWslIPAddress.Execute();
            } catch (Exception ex) {
                Assert.Fail(ex.Message);
            }

            Assert.True(true);
        }
    }
}