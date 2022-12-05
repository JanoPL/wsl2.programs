using Strategies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace StrategiesTest
{
    public class StrategiesServiceCollectionExtensionsTests
    {
        private (ServiceCollection, IConfigurationRoot) GetServiceConfiguration()
        {
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder().Build();

            return (services, configuration);
        }

        [Fact()]
        public void AddStrategiesTest()
        {
            (ServiceCollection services, IConfigurationRoot configuration) = GetServiceConfiguration();

            services.AddStrategies(configuration);

            Assert.True(services.Count >= 1);

            Assert.Collection<ServiceDescriptor>(services,
                item => Assert.Multiple(
                        () => Assert.Equal(ServiceLifetime.Scoped, item.Lifetime),
                        () => Assert.Equal(typeof(IContext), item.ServiceType)
                )
            );
        }

        [Fact()]
        public void AddStrategiesDependencyTest()
        {
            (ServiceCollection services, IConfigurationRoot configuration) = GetServiceConfiguration();

            services.AddStrategies();

            Assert.True(services.Count >= 1);

            Assert.Collection<ServiceDescriptor>(services,
                item => Assert.Multiple(
                        () => Assert.Equal(ServiceLifetime.Scoped, item.Lifetime),
                        () => Assert.Equal(typeof(IContext), item.ServiceType)
                )
            );
        }
    }
}