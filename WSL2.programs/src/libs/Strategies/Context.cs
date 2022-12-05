using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void ExecuteStrategies()
        {
            foreach (IStrategies strategy in _strategies) {
                strategy.Execute();
            }
        }
    }
}
