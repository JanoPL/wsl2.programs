namespace StrategiesTest.Strategies
{
    public class CheckWslIPAddressTests
    {
        private CheckWslIPAddress SetupService()
        {
            var wsl = new WslHelper();
            var logger = wsl.GetLogger();

            return new CheckWslIPAddress(logger, wsl.GetIWsl(wsl.GetSettings()));
        }

        [Fact]
        public void CheckWslIPAddressTest()
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