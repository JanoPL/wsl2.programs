using Firewall;
using HelperTest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WSL;

namespace WslTest
{
    public class WslServiceCollectionExtensionsTests
    {
        private (ServiceCollection, IConfigurationRoot) ServiceConfiguration
        {
            get
            {
                var services = new ServiceCollection();
                var configuration = new ConfigurationBuilder().Build();

                return (services, configuration);
            }
        }

        private ServiceCollection Service
        {
            get
            {
                ServiceCollection services = new();

                return services; 
            }
        }

        [Fact]
        public void AddWslWithConfigurationTest()
        {
            (ServiceCollection services, IConfigurationRoot configuration) = ServiceConfiguration;

            services.AddWsl(configuration);

            Assert.True(services.Count >= 1);

            IList<Type> types = new List<Type>() {
                typeof(IWsl),
            };

            ExtensionTestHelper.CheckServices(services, types);
        }

        [Fact]
        public void AddWslWithoutConfigurationTest()
        {
            ServiceCollection services = Service;

            services.AddWsl();

            Assert.True(services.Count >= 1);

            IList<Type> types = new List<Type>() {
                typeof(IWsl),
            };

            ExtensionTestHelper.CheckServices(services, types);
        }
    }
}