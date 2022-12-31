namespace StrategiesTest
{
    public class StrategiesTest
    {
        [Fact(DisplayName = "Test IStrategies Interface")]
        public void ExecuteTest()
        {
            Mock<IStrategies> mock = new();
            mock.SetupAllProperties();

            mock.Setup(m => m.Execute());

            mock.Object.Execute();

            mock.Verify(mock => mock.Execute(), Times.Once());
        }
    }
}
