using System.Reflection;
using Microsoft.CSharp.RuntimeBinder;

namespace HelperTest
{
    public static class StrategiesTestHelper
    {
        public static void ExecuteMethod(dynamic strategies)
        {
            try {
                strategies.Execute();
            } catch (RuntimeBinderException ex) {
                Assert.True(!string.IsNullOrEmpty(ex.Message), ex.Message);
                throw;
            }

            Assert.True(true);
        }
    }
}
