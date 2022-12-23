﻿using System.Runtime.Versioning;
using Firewall;
using HelperTest;
using Microsoft.Extensions.Logging;
using Moq;
using Strategies;

namespace StrategiesTest.Strategies
{
    [SupportedOSPlatform("windows")]
    public class DeleteFWRuleTests
    {
        [Fact]
        public void DeleteFWRuleTest()
        {
            FirewallHelper firewallHelper = new();

            var deleteFwRule = new DeleteFWRule(firewallHelper.GetRules(), firewallHelper.GetLogger());

            Assert.IsAssignableFrom<IStrategies>(deleteFwRule);
        }

        [Fact]
        public void ExecuteTest()
        {
            FirewallHelper firewallHelper = new();

            DeleteFWRule createFWRule = new(firewallHelper.GetRules(), firewallHelper.GetLogger());

            try {
                createFWRule.Execute();
            } catch (Exception ex) {
                Assert.Fail(ex.Message);
            }

            Assert.True(true);
        }
    }
}