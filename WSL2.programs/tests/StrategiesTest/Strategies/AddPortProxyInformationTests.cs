using Moq;
using Strategies;
using StrategiesTest.Helpers;
using WSL;

namespace StrategiesTest.Strategies
{
    public class AddPortProxyInformationTests
    {
        

        [Fact]
        public void AddPortProxyInformationTest()
        {
            var wsl = new WslHelper();

            AddPortProxyInformation proxyInformation = new AddPortProxyInformation(wsl.GetIWsl(wsl.GetSettings()));

            Assert.IsAssignableFrom<IStrategies>(proxyInformation);
        }

        [Fact]
        public void ExecuteTest()
        {
            var wsl = new WslHelper();

            AddPortProxyInformation proxyInformation = new AddPortProxyInformation(wsl.GetIWsl(wsl.GetSettings()));

            try {
                proxyInformation.Execute();
            } catch (Exception ex) {
                Assert.Fail(ex.Message);
            }

            Assert.True(true);
        }
    }
}