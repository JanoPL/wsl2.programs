using System.Runtime.Versioning;
using Firewall;
using Moq;
using Strategies;
using StrategiesTest.Helpers;

namespace StrategiesTest.Strategies
{
    [SupportedOSPlatform("windows")]
    public class DeleteFWRuleTests
    {
        [Fact]
        public void DeleteFWRuleTest()
        {
            FirewallHelper firewallHelper = new FirewallHelper();

            var deleteFwRule = new DeleteFWRule(firewallHelper.GetRules());

            Assert.IsAssignableFrom<IStrategies>(deleteFwRule);
        }

        [Fact]
        public void ExecuteTest()
        {
            FirewallHelper firewallHelper = new FirewallHelper();

            DeleteFWRule createFWRule = new DeleteFWRule(firewallHelper.GetRules());

            try {
                createFWRule.Execute();
            } catch (Exception ex) {
                Assert.Fail(ex.Message);
            }

            Assert.True(true);
        }
    }
}