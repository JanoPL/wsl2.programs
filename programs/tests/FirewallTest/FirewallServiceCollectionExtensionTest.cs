using System.Runtime.Versioning;
using Firewall;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WSL;

namespace FirewallTest
{
    [SupportedOSPlatform("windows")]
    public class FirewallServiceCollectionExtensionTest
    {
        private (ServiceCollection, IConfigurationRoot) GetServiceConfiguration()
        {
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder().Build();

            return (services, configuration);
        }

        [Fact]
        public void AddFirewallTest()
        {
            (ServiceCollection services, IConfigurationRoot configuration) = GetServiceConfiguration();

            services.AddFirewall(configuration);

            Assert.True(services.Count >= 2);

            Assert.Collection<ServiceDescriptor>(services,
                item => Assert.Multiple(
                        () => Assert.Equal(ServiceLifetime.Scoped, item.Lifetime),
                        () => Assert.Equal(typeof(IFirewall), item.ServiceType)
                ),
                item => Assert.Multiple(
                        () => Assert.Equal(ServiceLifetime.Scoped, item.Lifetime),
                        () => Assert.Equal(typeof(IWsl), item.ServiceType)
                )
            );
        }

        [Fact]
        public void AddFirewallTest1()
        {
            (ServiceCollection services, IConfigurationRoot configuration) = GetServiceConfiguration();

            services.AddFirewall();

            Assert.True(services.Count >= 2);

            Assert.Collection<ServiceDescriptor>(services,
                item => Assert.Multiple(
                        () => Assert.Equal(ServiceLifetime.Scoped, item.Lifetime),
                        () => Assert.Equal(typeof(IFirewall), item.ServiceType)
                ),
                item => Assert.Multiple(
                        () => Assert.Equal(ServiceLifetime.Scoped, item.Lifetime),
                        () => Assert.Equal(typeof(IWsl), item.ServiceType)
                )
            );
        }
    }
}
