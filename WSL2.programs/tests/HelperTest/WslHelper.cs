using Microsoft.Extensions.Logging;
using Moq;
using WSL;
using Xunit;

namespace HelperTest
{
    public class WslHelper : LoggerHelper
    {
        private readonly string ipAddress = "127.0.0.1";
        private readonly IList<string> ports = new List<string> { "22", "2222", "80", "443" };

        public string GetIpAddress()
        {
            return ipAddress;
        }

        public IList<string> GetPorts()
        {
            return ports;
        }

        public Settings GetSettings()
        {
            Mock<Settings> mockSettings = new();
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

        public IWsl GetIWsl(Settings settings)
        {
            Assert.NotNull(settings);

            Mock<IWsl> mockWsl = new();
            mockWsl.SetupAllProperties();
            mockWsl.Object.SetIpAddress(ipAddress);
            mockWsl.SetupGet(mObj => mObj.Settings).Returns(settings);

            IWsl wsl = mockWsl.Object;

            return wsl;
        }

        public static Wsl GetWsl()
        {
            Wsl wsl = new();

            return wsl;
        }
    }
}
