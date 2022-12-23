using System.Runtime.Versioning;
using Firewall;
using Moq;
using WSL;
using Xunit;

namespace HelperTest
{
    [SupportedOSPlatform("windows")]
    public class FirewallHelper : WslHelper
    {
        public Rules GetRules()
        {
            var settings = GetSettings();
            var mockWsl = GetIWsl(settings);

            Rules rules = new(mockWsl);

            return rules;
        }
    }
}
