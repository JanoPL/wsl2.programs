using HelperTest;
using Moq;
using Strategies;
using WSL;

namespace StrategiesTest.Strategies
{
    public class AddPortProxyInformationTests
    {
        

        [Fact]
        public void AddPortProxyInformationTest()
        {
            var wsl = new WslHelper();

            AddPortProxyInformation proxyInformation = new(wsl.GetIWsl(wsl.GetSettings()), wsl.GetLogger());

            Assert.IsAssignableFrom<IStrategies>(proxyInformation);
        }

        [Fact]
        public void ExecuteTest()
        {
            var wsl = new WslHelper();

            AddPortProxyInformation proxyInformation = new(wsl.GetIWsl(wsl.GetSettings()), wsl.GetLogger());

            try {
                proxyInformation.Execute();
            } catch (Exception ex) {
                Assert.Fail(ex.Message);
            }

            Assert.True(true);
        }
    }
}