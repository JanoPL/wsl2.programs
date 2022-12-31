namespace StrategiesTest.Strategies
{
    [SupportedOSPlatform("windows")]
    public class CreateFWRuleTests
    {
        private CreateFWRule SetupService()
        {
            FirewallHelper firewallHelper = new();
            var logger = new FirewallHelper().GetLogger();

            return new CreateFWRule(firewallHelper.GetRules(), logger);
        }

        [Fact]
        public void CreateFWRuleTest()
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