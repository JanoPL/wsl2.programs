using System.Diagnostics;
using System.Text;
using Moq;
using Strategies;

namespace StrategiesTest.Strategies
{
    public class ListUnitTest
    {
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

        private (ProcessStartInfo, Mock<Process>, Mock<StreamReader>) GetMocks()
        {
            ProcessStartInfo procStartinfo = GetProcessStartInfo();
            var mockProc = new Mock<Process>();
            var mockStreamReader = new Mock<StreamReader>();

            return (procStartinfo, mockProc, mockStreamReader);
        }

        [Fact]
        public void ExecuteTestWithEmptyOutput()
        {
            (ProcessStartInfo procStartInfo, Mock<Process> mockProc, Mock<StreamReader> mockStreamReader) = GetMocks();

            mockStreamReader.Setup(m => m.ReadLine()).Returns(string.Empty);

            var proc = mockProc.Object;
            proc.StartInfo = procStartInfo;
            var start = proc.Start();

            var line = mockProc.Object.StandardOutput.ReadLine();

            Assert.Equal(string.Empty, line);
        }

        //[Fact]
        //public void ExecuteTestWithNonEmptyOutput()
        //{
        //    (ProcessStartInfo procStartInfo, Mock<Process> mockProc, Mock<StreamReader> mockStreamReader) = GetMocks();

        //    string output = "\r\nListen on ipv4:             Connect to ipv4:\r\n\r\nAddress         Port        Address         Port\r\n--------------- ----------  --------------- ----------\r\n0.0.0.0         22          172.26.162.128  22\r\n0.0.0.0         80          172.26.162.128  80\r\n0.0.0.0         443         172.26.162.128  443\r\n0.0.0.0         8080        172.26.162.128  8080\r\n\r\n";

        //    mockStreamReader.Setup(m => m.ReadToEnd()).Returns(output);

        //    var proc = mockProc.Object;
        //    proc.StartInfo = procStartInfo;
        //    var start = proc.Start();

        //    Assert.True(start);

        //    StringBuilder read = new StringBuilder();

        //    while (!proc.StandardOutput.EndOfStream) {
        //        string? line = proc.StandardOutput.ReadLine();

        //        if (string.IsNullOrEmpty(line)) {
        //            read.AppendLine(line);
        //        }
        //    }

        //    Assert.Equal(output, read.ToString());
        //}

        [Fact(DisplayName = "Execute List Strategies")]
        public void ExecuteListStrategies()
        {
            var list = new List();
            try {
                list.Execute();
            } catch (Exception ex) {
                Assert.Fail(ex.Message);
            }

            Assert.True(true);
        }
    }
}
