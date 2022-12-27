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

            GetWslPorts checkWslIPAddress = new(logger);

            Assert.IsAssignableFrom<IStrategies>(checkWslIPAddress);
        }

        [Fact()]
        public void ExecuteTest()
        {
            var wsl = new WslHelper();
            var logger = wsl.GetLogger();

            GetWslPorts checkWslIPAddress = new(logger);

            try {
                checkWslIPAddress.Execute();
            } catch (Exception ex) {
                Assert.Fail(ex.Message);
            }

            Assert.True(true);
        }
    }
}