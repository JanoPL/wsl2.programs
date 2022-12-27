using HelperTest;
using Strategies;
using Strategies.Strategy;

namespace StrategiesTest.Strategies
{
    public class CheckUserRolesTests
    {
        [Fact]
        public void CheckUserRolesTest()
        {
            TestHelper testHelper = new TestHelper();
            var logger = testHelper.GetLogger();

            CheckUserRoles checkUserRoles = new(logger);

            Assert.IsAssignableFrom<IStrategies>(checkUserRoles);
        }

        [Fact]
        public void ExecuteTest()
        {
            var testHelper = new TestHelper();
            var logger = testHelper.GetLogger();

            CheckUserRoles checkUserRoles = new(logger);

            try {
                checkUserRoles.Execute();
            } catch (Exception ex) {
                Assert.Fail(ex.Message);
            }

            Assert.True(true);
        }
    }
}