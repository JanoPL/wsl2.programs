namespace StrategiesTest
{
    public class ContextTests
    {
        private static IStrategies GetStrategies()
        {
            Mock<IStrategies> mockStrategies = new();
            mockStrategies.SetupAllProperties();

            return mockStrategies.Object;
        }

        private static Mock<Context> GetMockContext()
        {
            Mock<Context> mockContext = new();
            mockContext.SetupAllProperties();

            return mockContext;
        }

        private static IContext GetContext()
        {
            Mock<Context> mockContext = ContextTests.GetMockContext();

            return mockContext.Object;
        }

        private static void AddStrategy(ref IContext context, IStrategies strategy)
        {
            context.AddStrategy(strategy);
        }


        [Fact]
        public void ContextTest()
        {
            Assert.IsAssignableFrom<Context>(ContextTests.GetContext());
        }

        [Fact]
        public void AddStrategyTest()
        {
            IContext context = ContextTests.GetContext();
            IStrategies strategies = ContextTests.GetStrategies();

            ContextTests.AddStrategy(ref context, strategies);

            Assert.Equal(1, context.Count());
        }

        [Fact]
        public void RemoveStrategyTest()
        {
            var context = ContextTests.GetContext();
            var strategies = ContextTests.GetStrategies();

            Assert.Equal(0, context.Count());

            ContextTests.AddStrategy(ref context, strategies);

            Assert.Equal(1, context.Count());

            context.RemoveStrategy(strategies);

            Assert.Equal(0, context.Count());
        }

        [Fact]
        public void CleanStrategiesTest()
        {
            var context = ContextTests.GetContext();
            int expected = 4;

            IList<IStrategies> list = new List<IStrategies>();

            for (int i = 1; i <= expected; i++) {
                var strategies = ContextTests.GetStrategies();
                list.Add(strategies);
            }

            Assert.Equal(expected, list.Count);

            foreach (var strategy in list) {
                ContextTests.AddStrategy(ref context, strategy);
            }

            Assert.Equal(expected, context.Count());

            context.CleanStrategies();

            Assert.Equal(0, context.Count());
        }

        [Fact]
        public void ExecuteStrategiesTest1()
        {
            var context = ContextTests.GetMockContext();
            var strategies = ContextTests.GetStrategies();

            context.Object.AddStrategy(strategies);
            context.Object.ExecuteStrategies();
            context.Verify(v => v.ExecuteStrategies(), Times.Once());
        }

        [Fact]

        public void ExecuteStrategiesTest2()
        {
            var context = new Context();
            var strategies = ContextTests.GetStrategies();
            context.AddStrategy(strategies);

            Assert.True(context.Count() > 0);

            context.ExecuteStrategies();
        }

        [Fact]
        public void CountTest()
        {
            var context = ContextTests.GetContext();
            Assert.IsType<int>(context.Count());
            Assert.Equal(0, context.Count());
        }
    }
}