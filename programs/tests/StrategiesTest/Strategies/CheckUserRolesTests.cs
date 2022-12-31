namespace StrategiesTest.Strategies
{
    public class CheckUserRolesTests
    {
        private CheckUserRoles SetupService()
        {
            TestHelper testHelper = new TestHelper();
            var logger = testHelper.GetLogger();

            return new CheckUserRoles(logger);
        }

        [Fact]
        public void CheckUserRolesTest()
        {
            Assert.IsAssignableFrom<IStrategies>(SetupService());
        }

        [Fact]
        public void ExecuteTest()
        {
            StrategiesTestHelper.ExecuteMethod(SetupService());
        }
    }
}