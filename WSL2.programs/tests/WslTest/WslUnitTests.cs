using HelperTest;
using WSL;

namespace WslTest
{
    public class WslUnitTests
    {
        private static void PortsAsserts(Wsl wsl, IList<string> testPorts)
        {
            Assert.Equal(testPorts, wsl.Settings.Ports);
            Assert.Equal(1, wsl.Settings.Ports.Count);
        }

        [Fact]
        public void WslTest()
        {
            Wsl wsl = new();
            Assert.IsAssignableFrom<IWsl>(wsl);
        }

        [Fact]
        public void TestSettingsIpAddress()
        {
            WslHelper wslHelper = new();
            Settings settings = wslHelper.GetSettings();

            IWsl wsl = wslHelper.GetIWsl(settings);

            Assert.Equal(wslHelper.GetIpAddress(), wsl.Settings.IpAddress);
        }

        [Fact]
        public void TestSettingsPort()
        {
            WslHelper wslHelper = new();
            Settings settings = wslHelper.GetSettings();
            IWsl wsl = wslHelper.GetIWsl(settings);

            Assert.Equal(wslHelper.GetPorts(), wsl.Settings.Ports);
        }

        [Fact]
        public void SetIpAddressTest()
        {
            WslHelper wslHelper = new();
            Wsl wsl = WslHelper.GetWsl();

            wsl.SetIpAddress("123.123.123.123");

            Assert.Equal("123.123.123.123", wsl.Settings.IpAddress);
        }

        [Fact]
        public void AddPortIntegerTest()
        {
             WslHelper wslHelper = new();
            Wsl wsl = WslHelper.GetWsl();
            IList<string> testPorts = new List<string> { "20" };

            wsl.AddPort(20);

            PortsAsserts(wsl, testPorts);
        }

        [Fact]
        public void AddPortStringTest()
        {
             WslHelper wslHelper = new();
            Wsl wsl = WslHelper.GetWsl();
            IList<string> testPorts = new List<string> { "20" };

            wsl.AddPort("20");

            PortsAsserts(wsl, testPorts);
        }

        [Fact]
        public void SetPortsTest()
        {
             WslHelper wslHelper = new();
            Wsl wsl = WslHelper.GetWsl();

            wsl.SetPorts(wslHelper.GetPorts());

            Assert.Equal(wslHelper.GetPorts(), wsl.Settings.Ports);
        }
    }
}