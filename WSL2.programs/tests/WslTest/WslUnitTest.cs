using WSL;
using Moq;
namespace WslTest
{
    public class WslUnitTest
    {
        private string ipAddress = "127.0.0.1";
        private string[] ports = { "22", "2222", "80", "443" };

        private Settings GetSettings()
        {
            Mock<Settings> mockSettings = new Mock<Settings>();
            mockSettings.SetupAllProperties();
            mockSettings.Object.IpAddress = ipAddress;
            mockSettings.Object.Ports = ports;

            Settings settings = mockSettings.Object;

            Assert.Equal(ipAddress, settings.IpAddress);
            Assert.Equal(ipAddress, mockSettings.Object.IpAddress);
            Assert.Equal(ports, settings.Ports);
            Assert.Equal(ports, mockSettings.Object.Ports);

            return settings;
        }

        private IWsl GetWsl(Settings settings)
        {
            Assert.NotNull(settings);

            Mock<IWsl> mockWsl = new Mock<IWsl>();
            mockWsl.SetupAllProperties();
            mockWsl.Object.SetIpAddress(ipAddress);
            mockWsl.SetupGet(mObj => mObj.Settings).Returns(settings);

            IWsl wsl = mockWsl.Object;

            return wsl;
        }

        [Fact]
        public void TestSettingsIpAddress()
        {
            Settings settings = GetSettings();

            IWsl wsl = GetWsl(settings);

            Assert.Equal(ipAddress, wsl.Settings.IpAddress);
        }

        [Fact]
        public void TestSettingsPort()
        {
            Settings settings = GetSettings();
            IWsl wsl = GetWsl(settings);

            Assert.Equal(ports, wsl.Settings.Ports);
        }

        public void 
    }
}