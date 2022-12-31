namespace SolutionTest
{
    public class HelperTests
    {
        [Fact]
        public void StrategiesTestHelperRuntimeBinderExTest()
        {
            Mock<CheckUserRoles> checkUserRoles = new Mock<CheckUserRoles>();

            Assert.Throws<RuntimeBinderException>(() => StrategiesTestHelper.ExecuteMethod(checkUserRoles));
        }
    }
}