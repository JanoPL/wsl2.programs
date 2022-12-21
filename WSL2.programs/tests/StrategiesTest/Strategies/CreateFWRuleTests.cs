using System.Runtime.Versioning;
using Firewall;
using Moq;
using Strategies;
using StrategiesTest.Helpers;

namespace StrategiesTest.Strategies
{
    [SupportedOSPlatform("windows")]
    public class CreateFWRuleTests
    {
        [Fact()]
        public void CreateFWRuleTest()
        {
            Mock<IFirewall> mock = new();

            var createFwRule = new CreateFWRule(mock.Object);

            Assert.IsAssignableFrom<IStrategies>(createFwRule);
        }

        [Fact()]
        public void ExecuteTest()
        {
            FirewallHelper firewallHelper = new FirewallHelper();

            CreateFWRule createFWRule = new CreateFWRule(firewallHelper.GetRules());

            try {
                createFWRule.Execute();
            } catch (Exception ex) {
                Assert.Fail(ex.Message);
            }

            Assert.True(true);
        }
    }
}