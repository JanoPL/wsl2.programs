extern alias test_NetFwTypeLib;
using System.Runtime.Versioning;
using Firewall;
using FluentAssertions;
using Moq;
using WSL;

namespace FirewallTest
{
    [SupportedOSPlatform("windows")]
    public class FirewallUnitTest
    {
        private readonly string ipAddress = "127.0.0.1";
        private readonly IList<string> ports = new List<string>() { "22", "2222", "80", "443" };
        private Settings GetSettings()
        {
            Mock<Settings> mockSettings = new Mock<Settings>();
            mockSettings.SetupAllProperties();
            mockSettings.Object.IpAddress = ipAddress;
            mockSettings.Object.Ports = ports;

            mockSettings.Setup(ms => ms.IpAddress).Returns(ipAddress);
            mockSettings.Setup(ms => ms.Ports).Returns(ports);

            Settings settings = mockSettings.Object;

            Assert.Equal(ipAddress, settings.IpAddress);
            Assert.Equal(ipAddress, mockSettings.Object.IpAddress);
            Assert.Equal(ports, settings.Ports);
            Assert.Equal(ports, mockSettings.Object.Ports);

            return settings;
        }

        private IWsl GetIWsl(Settings settings)
        {
            Assert.NotNull(settings);

            Mock<IWsl> mockWsl = new Mock<IWsl>();
            mockWsl.SetupAllProperties();
            mockWsl.Object.SetIpAddress(ipAddress);
            mockWsl.SetupGet(mObj => mObj.Settings).Returns(settings);

            IWsl wsl = mockWsl.Object;

            return wsl;
        }

        private Rules GetRules()
        {
            var settings = GetSettings();
            var mockWsl = GetIWsl(settings);

            Rules rules = new Rules(mockWsl);

            return rules;
        }

        [Fact()]
        public void RulesTest()
        {
            var rules = GetRules();

            Assert.IsAssignableFrom<IFirewall>(rules);
            Assert.NotNull(rules.Elements);
        }

        [Fact()]
        public void BuildInboundTest()
        {
            var rules = GetRules().BuildInbound();

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

        [Fact()]
        public void BuildOutboundTest()
        {
            var rules = GetRules().BuildOutbound();

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
            var rules = GetRules().BuildOutbound().BuildInbound();

            Assert.NotNull(rules.Elements);

            Assert.True(rules.Elements.Count == 2);
        }
    }
}