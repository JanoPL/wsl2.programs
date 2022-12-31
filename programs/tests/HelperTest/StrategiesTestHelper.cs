using System.Reflection;

namespace HelperTest
{
    public static class StrategiesTestHelper
    {
        public static void ExecuteMethod(dynamic strategies)
        {
            try {
                strategies.Execute();
            } catch (Exception ex) {
                Assert.Fail(ex.Message);
            }

            Assert.True(true);
        }
    }
}
