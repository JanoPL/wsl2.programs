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

        [Fact]
        public void AddStrategiesDependencyWithoutConfigurationTest()
        {
            (ServiceCollection services, IConfigurationRoot configuration) = GetServiceConfiguration();

            services.AddStrategies();

            Assert.True(services.Count >= 1);

            IList<Type> types = new List<Type> {
                typeof(IContext),
                typeof(IPorts)
            };

            ExtensionTestHelper.CheckServices(services, types);
        }

        [Fact]
        public void AddStrategiesDependencyWithConfigurationTest()
        {
            (ServiceCollection services, IConfigurationRoot configuration) = GetServiceConfiguration();

            services.AddStrategies(configuration);

            Assert.True(services.Count >= 1);

            IList<Type> types = new List<Type> {
                typeof(IContext),
                typeof(IPorts)
            };

            ExtensionTestHelper.CheckServices(services, types);
        }
    }
}