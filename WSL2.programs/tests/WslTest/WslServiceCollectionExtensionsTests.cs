using WSL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WslTest
{
    public class WslServiceCollectionExtensionsTests
    {
        private (ServiceCollection, IConfigurationRoot) GetServiceConfiguration()
        {
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder().Build();

            return (services, configuration);
        }

        [Fact()]
        public void AddWslTest()
        {
            (ServiceCollection services, IConfigurationRoot configuration) = GetServiceConfiguration();

            services.AddWsl(configuration);

            Assert.True(services.Count >= 1);

            Assert.Collection<ServiceDescriptor>(services,
                item => Assert.Multiple(
                        () => Assert.Equal(ServiceLifetime.Scoped, item.Lifetime),
                        () => Assert.Equal(typeof(IWsl), item.ServiceType)
                )
            );
        }

        [Fact()]
        public void AddWslTest1()
        {
            (ServiceCollection services, IConfigurationRoot configuration) = GetServiceConfiguration();

            services.AddWsl();

            Assert.True(services.Count >= 1);

            Assert.Collection<ServiceDescriptor>(services,
                item => Assert.Multiple(
                        () => Assert.Equal(ServiceLifetime.Scoped, item.Lifetime),
                        () => Assert.Equal(typeof(IWsl), item.ServiceType)
                )
            );
        }
    }
}