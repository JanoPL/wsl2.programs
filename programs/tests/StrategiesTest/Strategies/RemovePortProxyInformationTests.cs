namespace StrategiesTest.Strategies
{
    public class RemovePortProxyInformationTests
    {
        private RemovePortProxyInformation SetupService()
        {
            var wsl = new WslHelper();

            return new RemovePortProxyInformation(wsl.GetIWsl(wsl.GetSettings()), wsl.GetLogger());
        }

        [Fact]
        public void RemovePortProxyInformationTest()
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