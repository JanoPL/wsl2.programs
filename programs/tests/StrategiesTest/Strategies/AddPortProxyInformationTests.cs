namespace StrategiesTest.Strategies
{
    public class AddPortProxyInformationTests
    {
        private AddPortProxyInformation SetupService()
        {
            var wsl = new WslHelper();

            return new AddPortProxyInformation(wsl.GetIWsl(wsl.GetSettings()), wsl.GetLogger());
        }

        [Fact]
        public void AddPortProxyInformationTest()
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