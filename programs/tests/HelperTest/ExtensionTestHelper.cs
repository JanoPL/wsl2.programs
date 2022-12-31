namespace HelperTest
{
    public class ExtensionTestHelper
    {
        public static void CheckServices(ServiceCollection services, IList<Type> types)
        {
            IList<Action<ServiceDescriptor>> actions = new List<Action<ServiceDescriptor>>();

            foreach (var type in types) {
                IList<Action> checks = new List<Action>();

                ServiceDescriptor? service = services.Where(service => service.ServiceType.Name == type.Name).FirstOrDefault();

                if (service != null) {
                    checks.Add(() => Assert.Equal(ServiceLifetime.Scoped, service.Lifetime));

                    checks.Add(() => Assert.Equal(type, service.ServiceType));

                    actions.Add(item => Assert.Multiple(checks.ToArray()));
                } else {
                    Assert.Fail($"The service Collection has not typed service: {type.Name}");
                }

                //foreach (ServiceDescriptor service in services) {
                //        checks.Add(() => Assert.Equal(ServiceLifetime.Scoped, service.Lifetime));

                //        checks.Add(() => Assert.Equal(type, service.ServiceType));
                //}

                //actions.Add(item => Assert.Multiple(checks.ToArray()));
            }

            Assert.Collection<ServiceDescriptor>(services, actions.ToArray());
        }
    }
}
