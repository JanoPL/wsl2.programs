extern alias test_NetFwTypeLib;
using System.Runtime.Versioning;
using Firewall;
using FluentAssertions;
using HelperTest;

namespace FirewallTest
{
    [SupportedOSPlatform("windows")]
    public class FirewallUnitTest
    {
        [Fact]
        public void RulesTest()
        {
            var firewallHelpers = new FirewallHelper();
            var rules = firewallHelpers.GetRules();

            Assert.IsAssignableFrom<IFirewall>(rules);
            Assert.NotNull(rules.Elements);
        }

        [Fact]
        public void BuildInboundTest()
        {
            var firewallHelpers = new FirewallHelper();
            var rules = firewallHelpers.GetRules().BuildInbound();

            Assert.NotNull(rules.Elements);

            // retriev information from Syste.__ComObject
            var rulesType = Microsoft.VisualBasic.Information.TypeName(rules.Elements.First());

            rulesType
                .Should()
                .BeOneOf(
                    typeof(test_NetFwTypeLib.NetFwTypeLib.INetFwRule).Name,
                    typeof(test_NetFwTypeLib.NetFwTypeLib.INetFwRule2).Name,
                    typeof(test_NetFwTypeLib.NetFwTypeLib.INetFwRule3).Name
                );

            Assert.True(rules.Elements.Count > 0);
        }

        [Fact]
        public void BuildOutboundTest()
        {
            var firewallHelpers = new FirewallHelper();
            var rules = firewallHelpers.GetRules().BuildOutbound();

            Assert.NotNull(rules.Elements);

            // retriev information from Syste.__ComObject
            var rulesType = Microsoft.VisualBasic.Information.TypeName(rules.Elements.First());

            rulesType
                .Should()
                .BeOneOf(
                    typeof(test_NetFwTypeLib.NetFwTypeLib.INetFwRule).Name,
                    typeof(test_NetFwTypeLib.NetFwTypeLib.INetFwRule2).Name,
                    typeof(test_NetFwTypeLib.NetFwTypeLib.INetFwRule3).Name
                );

            Assert.True(rules.Elements.Count > 0);
        }

        [Fact]
        public void BuildAllRules()
        {
            var firewallHelpers = new FirewallHelper();
            var rules = firewallHelpers.GetRules().BuildOutbound().BuildInbound();

            Assert.NotNull(rules.Elements);

            Assert.True(rules.Elements.Count == 2);
        }
    }
}