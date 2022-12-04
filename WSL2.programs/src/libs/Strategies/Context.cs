using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategies
{
    public class Context
    {
        private IList<IStrategies> _strategies = new List<IStrategies>();

        public Context() { }

        public void AddStrategy(IStrategies strategy)
        {
            _strategies.Add(strategy);
        }

        public void removeStrategy(IStrategies strategy)
        {
            _strategies.Remove(strategy);
        }

        public void cleanStrategies()
        {
            _strategies.Clear();
        }

        public void ExecuteStrategy()
        {
            foreach (IStrategies strategy in _strategies) {
                strategy.Execute();
            }
        }
    }
}
