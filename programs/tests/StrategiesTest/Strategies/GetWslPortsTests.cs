namespace StrategiesTest.Strategies
{
    public class GetWslPortsTests
    {
        private GetWslPorts SetupService()
        {
            var wsl = new WslHelper();
            var logger = wsl.GetLogger();
            var ports = wsl.GetPortsList();

            return new GetWslPorts(logger, ports);
        }
        [Fact]
        public void GetWslPortsTest()
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