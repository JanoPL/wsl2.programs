using System.Runtime.Versioning;
using Firewall;
using HelperTest;
using Moq;
using Strategies;

namespace StrategiesTest.Strategies
{
    [SupportedOSPlatform("windows")]
    public class CreateFWRuleTests
    {
        [Fact]
        public void CreateFWRuleTest()
        {
            FirewallHelper firewallHelper = new();
            var logger = new FirewallHelper().GetLogger();

            CreateFWRule createFwRule = new(firewallHelper.GetRules(), logger);

            Assert.IsAssignableFrom<IStrategies>(createFwRule);
        }

        [Fact]
        public void ExecuteTest()
        {
            FirewallHelper firewallHelper = new();

            CreateFWRule createFWRule = new(firewallHelper.GetRules(), firewallHelper.GetLogger());

            try {
                createFWRule.Execute();
            } catch (Exception ex) {
                Assert.Fail(ex.Message);
            }

            Assert.True(true);
        }
    }
}