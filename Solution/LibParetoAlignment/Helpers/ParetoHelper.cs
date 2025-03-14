using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibParetoAlignment.Helpers
{
    internal class ParetoHelper
    {
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
    }
}
