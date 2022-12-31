namespace StrategiesTest.Strategies
{
    [SupportedOSPlatform("windows")]
    public class DeleteFWRuleTests
    {
        private DeleteFWRule SetupService()
        {
            FirewallHelper firewallHelper = new();

            return new DeleteFWRule(firewallHelper.GetRules(), firewallHelper.GetLogger());
        }

        [Fact]
        public void DeleteFWRuleTest()
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