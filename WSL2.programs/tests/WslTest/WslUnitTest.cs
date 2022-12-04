using Xunit;
using WSL;
using Moq;
using Castle.Components.DictionaryAdapter.Xml;

namespace WslTest
{
    public class WslUnitTest
    {
        private string ipAddress = "127.0.0.1";
        private IList<string> ports = new List<string>() { "22", "2222", "80", "443" };

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

        private Wsl GetWsl()
        {
            Wsl wsl = new();

            return wsl;
        }

        private void PortsAsserts(Wsl wsl, IList<string> testPorts)
        {
            Assert.Equal(testPorts, wsl.Settings.Ports);
            Assert.Equal(1, wsl.Settings.Ports.Count);
        }

        [Fact()]
        public void WslTest()
        {
            Wsl wsl = new Wsl();
            Assert.IsAssignableFrom<IWsl>(wsl);
        }

        [Fact()]
        public void TestSettingsIpAddress()
        {
            Settings settings = GetSettings();

            IWsl wsl = GetIWsl(settings);

            Assert.Equal(ipAddress, wsl.Settings.IpAddress);
        }

        [Fact()]
        public void TestSettingsPort()
        {
            Settings settings = GetSettings();
            IWsl wsl = GetIWsl(settings);

            Assert.Equal(ports, wsl.Settings.Ports);
        }

        [Fact()]
        public void SetIpAddressTest()
        {
            Settings settings = GetSettings();
            Wsl wsl = GetWsl(/*settings*/);

            wsl.SetIpAddress("123.123.123.123");

            Assert.Equal("123.123.123.123", wsl.Settings.IpAddress);
        }

        [Fact()]
        public void AddPortIntegerTest()
        {
            Wsl wsl = GetWsl();
            IList<string> testPorts = new List<string>() { "20" };

            wsl.AddPort(20);

            PortsAsserts(wsl, testPorts);
        }

        [Fact]
        public void AddPortStringTest()
        {
            Wsl wsl = GetWsl();
            IList<string> testPorts = new List<string>() { "20" };

            wsl.AddPort("20");

            PortsAsserts(wsl, testPorts);
        }

        [Fact()]
        public void SetPortsTest()
        {
            Wsl wsl = GetWsl();

            wsl.SetPorts(ports);

            Assert.Equal(ports, wsl.Settings.Ports);
        }
    }
}