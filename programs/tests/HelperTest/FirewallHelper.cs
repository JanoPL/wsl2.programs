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
