namespace HelperTest
{
    public abstract class LoggerHelper
    {
#pragma warning disable CA1822
        public ILogger GetLogger()
        {
            var mockLogger = new Mock<ILogger>();

            return mockLogger.Object;
        }
#pragma warning restore CA1822

        public ILogger<T> GetLogger<T>()
        {
            var mockLogger = new Mock<ILogger<T>>();

            return mockLogger.Object;
        }

        public IPorts GetPortsList()
        {
            var mockListPortsObject = new Mock<IPorts>();

            return mockListPortsObject.Object;
        }
    }
}
