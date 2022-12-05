namespace Strategies
{
    public interface IContext
    {
        public void AddStrategy(IStrategies strategy);
        public void RemoveStrategy(IStrategies strategy);
        public void CleanStrategies();
        public void ExecuteStrategies();
    }
}