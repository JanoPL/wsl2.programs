namespace HelperTest
{
    public static class ExtensionTestHelper
    {
        public static void CheckServices(ServiceCollection services, IList<Type> types)
        {
            IList<Action<ServiceDescriptor>> actions = new List<Action<ServiceDescriptor>>();

            foreach (var type in types) {
                IList<Action> checks = new List<Action>();

                ServiceDescriptor? service = services.Where(service => service.ServiceType.Name == type.Name).FirstOrDefault();

                try {
                    checks.Add(() => Assert.Equal(ServiceLifetime.Scoped, service?.Lifetime));

                    checks.Add(() => Assert.Equal(type, service?.ServiceType));

                    actions.Add(item => Assert.Multiple(checks.ToArray()));
                } catch (NullReferenceException) {
                    throw;
                }
            }

            Assert.Collection<ServiceDescriptor>(services, actions.ToArray());
        }
    }
}
