namespace StrategiesTest.Strategies
{
    [SupportedOSPlatform("windows")]
    public class ListUnitTest
    {
        private List SetupService()
        {
            var logger = new FirewallHelper().GetLogger();

            return new List(logger);
        }

        private static ProcessStartInfo GetProcessStartInfo()
        {
            ProcessStartInfo processStartInfo = new() {
                FileName = "netsh.exe",
                Arguments = "interface portproxy show v4tov4",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
            };

            return processStartInfo;
        }

        private static (ProcessStartInfo, Mock<Process>, Mock<StreamReader>) GetMocks()
        {
            ProcessStartInfo procStartinfo = GetProcessStartInfo();
            var mockProc = new Mock<Process>();
            var mockStreamReader = new Mock<StreamReader>();

            return (procStartinfo, mockProc, mockStreamReader);
        }

        [Fact]
        public void ExecuteTestWithEmptyOutput()
        {
            (ProcessStartInfo procStartInfo, Mock<Process> mockProc, Mock<StreamReader> mockStreamReader) = ListUnitTest.GetMocks();

            mockStreamReader.Setup(m => m.ReadLine()).Returns(string.Empty);

            var proc = mockProc.Object;
            proc.StartInfo = procStartInfo;
            var start = proc.Start();

            Assert.True(start);

            var line = mockProc.Object.StandardOutput.ReadLine();

            Assert.Equal(string.Empty, line);
        }

        [Fact(DisplayName = "Execute List Strategies")]
        public void ExecuteListStrategies()
        {
            StrategiesTestHelper.ExecuteMethod(SetupService());
        }
    }
}
