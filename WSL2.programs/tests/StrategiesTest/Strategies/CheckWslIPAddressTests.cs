
using Microsoft.Extensions.Logging;
using Moq;
using Strategies;
using StrategiesTest.Helpers;

namespace StrategiesTest.Strategies
{
    public class CheckWslIPAddressTests
    {
        [Fact]
        public void CheckWslIPAddressTest()
        {
            var wsl = new WslHelper();
            var mockLogger = new Mock<ILogger<CheckWslIPAddress>>();
            var logger = mockLogger.Object;

            CheckWslIPAddress checkWslIPAddress = new(logger, wsl.GetIWsl(wsl.GetSettings()));

            Assert.IsAssignableFrom<IStrategies>(checkWslIPAddress);
        }

        [Fact]
        public void ExecuteTest()
        {
            var wsl = new WslHelper();
            var mockLogger = new Mock<ILogger<CheckWslIPAddress>>();
            var logger = mockLogger.Object;

            CheckWslIPAddress checkWslIPAddress = new(logger, wsl.GetIWsl(wsl.GetSettings()));
            
            try {
                checkWslIPAddress.Execute();
            } catch (Exception ex) {
                Assert.Fail(ex.Message);
            }

            Assert.True(true);
        }
    }
}