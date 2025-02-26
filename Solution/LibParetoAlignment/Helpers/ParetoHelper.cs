using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibParetoAlignment.Helpers
{
    internal class ParetoHelper
    {
        public bool SolutionIsNonDominated(TradeoffAlignment solution, List<TradeoffAlignment> population)
        {
            foreach(TradeoffAlignment existingSolution in population)
            {
                bool isDominated = ADominatesB(existingSolution, solution);
                if (isDominated)
                {
                    return false;
                }
            }

            return true;
        }

        public bool ADominatesB(TradeoffAlignment a, TradeoffAlignment b, List<string> objectives)
        {
            bool includesImprovement = false;

            foreach(string objective in objectives)
            {
                double aScore = a.Scores[objective];
                double bScore = b.Scores[objective];

                if (aScore > bScore)
                {
                    includesImprovement = true;
                }
                else if (aScore < bScore)
                {
                    return false;
                }
            }

            return includesImprovement;
        }

        public bool ADominatesB(TradeoffAlignment a, TradeoffAlignment b)
        {
            List<string> objectives = a.Scores.Keys.ToList();
            return ADominatesB(a, b, objectives);
        }


        public bool ADominatesB(DominationDecorator a, DominationDecorator b)
        {
            return ADominatesB(a.Tradeoff, b.Tradeoff);
        }
    }
}
