using System.Text;
using HelperTest;
using Ports;
using Ports.Models;

namespace PortsTest
{
    public class PortsServiceTests
    {
        private static string GetJsonToParse()
        {
           return "{\n   \"table\": [\n      {\n         \"state\": \"LISTEN\",\n         \"recv-q\": \"0\",\n         \"send-q\": \"128\",\n         \"local\": \"0.0.0.0:22\",\n         \"address:port\": \"0.0.0.0:*\",\n         \"peer\": null,\n         \"address:portprocess\": null\n      }\n   ]\n}\n";
        }

        private static string GetJsonFromParse()
        {
            return "LISTEN 0 128 0.0.0.0:22 0.0.0.0:*  \r\n";
        }

        private PortsService GetService()
        {
            TestHelper testHelper = new();
            var logger = testHelper.GetLogger<PortsService>();
            return new PortsService(logger);
        }

        [Fact]
        public void ParseAsStringNewLineTest()
        {
            PortsService portsService = GetService();

            string? result = portsService.ParseAsString(GetJsonToParse(), true);

            Assert.NotNull(result);

            Assert.Equal(GetJsonFromParse(), result);
        }

        [Fact]
        public void ParseAsStringInlineTest()
        {
            PortsService portsService = GetService();

            string? result = portsService.ParseAsString(GetJsonToParse(), false);

            Assert.NotNull(result);

            Assert.Equal(GetJsonFromParse(), result);
        }

        [Fact]
        public void ParseAsObjectTest()
        {
            PortsService portsService = GetService();

            var result = portsService.ParseAsObject(GetJsonToParse());

            Assert.NotNull(result);
            Assert.IsType<PortTable>(result);
        }

        [Fact]
        public void ParseAsLoggerTest()
        {
            PortsService portsService = GetService();

            try {
                portsService.ParseAsLogger(GetJsonToParse());
            } catch (Exception ex) {
                Assert.Fail(ex.Message);
            }

            Assert.True(true);
        }
    }
}