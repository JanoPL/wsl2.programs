using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ports;

namespace PortsTest
{
    public class PortsServiceCollectionExtensionTests
    {
        private (ServiceCollection, IConfigurationRoot) GetServiceConfiguration()
        {
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder().Build();

            return (services, configuration);
        }

        [Fact]
        public void AddPortsWithConfigurationTest()
        {
            (ServiceCollection services, IConfigurationRoot configuration) = GetServiceConfiguration();

            services.AddPorts(configuration);

            Assert.True(services.Count >= 1);

            Assert.Collection<ServiceDescriptor>(services,
                item => Assert.Multiple(
                        () => Assert.Equal(ServiceLifetime.Scoped, item.Lifetime),
                        () => Assert.Equal(typeof(IPorts), item.ServiceType)
                )
            );
        }

        [Fact]
        public void AddPortsTestWithoutConfiguration()
        {
            (ServiceCollection services, IConfigurationRoot configuration) = GetServiceConfiguration();

            services.AddPorts();

            Assert.True(services.Count >= 1);

            Assert.Collection<ServiceDescriptor>(services,
                item => Assert.Multiple(
                        () => Assert.Equal(ServiceLifetime.Scoped, item.Lifetime),
                        () => Assert.Equal(typeof(IPorts), item.ServiceType)
                )
            );
        }
    }
}