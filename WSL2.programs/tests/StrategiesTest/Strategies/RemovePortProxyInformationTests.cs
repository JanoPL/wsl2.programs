using HelperTest;
using Strategies;

namespace StrategiesTest.Strategies
{
    public class RemovePortProxyInformationTests
    {
        [Fact]
        public void RemovePortProxyInformationTest()
        {
            var wsl = new WslHelper();

            RemovePortProxyInformation proxyInformation = new RemovePortProxyInformation(wsl.GetIWsl(wsl.GetSettings()));

            Assert.IsAssignableFrom<IStrategies>(proxyInformation);
        }

        [Fact]
        public void ExecuteTest()
        {
            var wsl = new WslHelper();

            RemovePortProxyInformation proxyInformation = new RemovePortProxyInformation(wsl.GetIWsl(wsl.GetSettings()));

            try {
                proxyInformation.Execute();
            } catch (Exception ex) {
                Assert.Fail(ex.Message);
            }

            Assert.True(true);
        }
    }
}