using Xunit;
using Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Diagnostics;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;

namespace StrategiesTest
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

        [Fact()]
        public void ExecuteTestWithEmptyOutput()
        {
            var procStartinfo = GetProcessStartInfo();
            var mockProc = new Mock<Process>();
            var mockStreamReader = new Mock<StreamReader>();

            mockStreamReader.Setup(m => m.ReadLine()).Returns(string.Empty);

            var proc = mockProc.Object;
            proc.StartInfo = procStartinfo;
            var start = proc.Start();

            var line = mockProc.Object.StandardOutput.ReadLine();

            Assert.Equal(string.Empty, line);
        }

        [Fact]
        public void ExecuteTestWithNonEmptyOutput()
        {
            var procStartinfo = GetProcessStartInfo();
            var mockProc = new Mock<Process>();
            var mockStreamReader = new Mock<StreamReader>();

            string output = "\r\nListen on ipv4:             Connect to ipv4:\r\n\r\nAddress         Port        Address         Port\r\n--------------- ----------  --------------- ----------\r\n0.0.0.0         22          172.26.162.128  22\r\n0.0.0.0         80          172.26.162.128  80\r\n0.0.0.0         443         172.26.162.128  443\r\n0.0.0.0         8080        172.26.162.128  8080\r\n\r\n";

            mockStreamReader.Setup(m => m.ReadToEnd()).Returns(output);

            var proc = mockProc.Object;
            proc.StartInfo = procStartinfo;
            var start = proc.Start();

            var read = mockProc.Object.StandardOutput.ReadToEnd();

            Assert.Equal(output, read);
        }

        [Fact(DisplayName = "Execute List Strategies")]
        public void ExecuteListStrategies()
        {
            var list = new List();
            try {
                list.Execute();
            } catch(Exception ex) {
                Assert.Fail(ex.Message);
            }

            Assert.True(true);
        }
    }
}
