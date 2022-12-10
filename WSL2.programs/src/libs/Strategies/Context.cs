namespace Strategies
{
    public class Context : IContext
    {
        private IList<IStrategies> _strategies = new List<IStrategies>();

        public Context() { }

        public void AddStrategy(IStrategies strategy)
        {
            _strategies.Add(strategy);
        }

        public void RemoveStrategy(IStrategies strategy)
        {
            _strategies.Remove(strategy);
        }

        public void CleanStrategies()
        {
            _strategies.Clear();
        }

        virtual public void ExecuteStrategies()
        {
            foreach (IStrategies strategy in _strategies) {
                strategy.Execute();
            }
        }

        public int Count()
        {
            return _strategies.Count;
        }
    }
}
