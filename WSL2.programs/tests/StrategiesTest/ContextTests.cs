using Moq;
using Strategies;

namespace StrategiesTest
{
    public class ContextTests
    {
        private IStrategies GetStrategies()
        {
            Mock<IStrategies> mockStrategies = new Mock<IStrategies>();
            mockStrategies.SetupAllProperties();

            return mockStrategies.Object;
        }

        private Mock<Context> GetMockContext()
        {
            Mock<Context> mockContext = new Mock<Context>();
            mockContext.SetupAllProperties();

            return mockContext;
        }

        private IContext GetContext()
        {
            Mock<Context> mockContext = GetMockContext();

            return mockContext.Object;
        }

        private void AddStrategy(ref IContext context, IStrategies strategy)
        {
            context.AddStrategy(strategy);
        }


        [Fact]
        public void ContextTest()
        {
            Assert.IsAssignableFrom<Context>(GetContext());
        }

        [Fact]
        public void AddStrategyTest()
        {
            IContext context = GetContext();
            IStrategies strategies = GetStrategies();

            AddStrategy(ref context, strategies);

            Assert.Equal(1, context.Count());
        }

        [Fact]
        public void RemoveStrategyTest()
        {
            var context = GetContext();
            var strategies = GetStrategies();

            Assert.Equal(0, context.Count());

            AddStrategy(ref context, strategies);

            Assert.Equal(1, context.Count());

            context.RemoveStrategy(strategies);

            Assert.Equal(0, context.Count());
        }

        [Fact]
        public void CleanStrategiesTest()
        {
            var context = GetContext();
            int expected = 4;

            IList<IStrategies> list = new List<IStrategies>();

            for (int i = 1; i <= expected; i++) {
                var strategies = GetStrategies();
                list.Add(strategies);
            }

            Assert.Equal(expected, list.Count());

            foreach (var strategy in list) {
                AddStrategy(ref context, strategy);
            }

            Assert.Equal(expected, context.Count());

            context.CleanStrategies();

            Assert.Equal(0, context.Count());
        }

        [Fact]
        public void ExecuteStrategiesTest()
        {
            var context = GetMockContext();
            var strategies = GetStrategies();

            context.Object.AddStrategy(strategies);
            context.Object.ExecuteStrategies();
            context.Verify(v => v.ExecuteStrategies());
        }

        [Fact]
        public void CountTest()
        {
            var context = GetContext();
            Assert.IsType<int>(context.Count());
            Assert.Equal(0, context.Count());
        }
    }
}