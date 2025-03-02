using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace LibParetoAlignment.Helpers
{
    public class CrowdedComparisonOperator
    {
        public static void SortTradeoffs(List<TradeoffAlignment> tradeoffs)
        {
            int n = tradeoffs.Count;
            for (int i = 0; i < n - 1; i++)
            {
                int currentBestIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (PreferAOverB(tradeoffs[j], tradeoffs[currentBestIndex]))
                    {
                        currentBestIndex = j;
                    }
                }

                TradeoffAlignment temp = tradeoffs[i];
                tradeoffs[i] = tradeoffs[currentBestIndex];
                tradeoffs[currentBestIndex] = temp;
            }
        }

        public static bool PreferAOverB(TradeoffAlignment a, TradeoffAlignment b)
        {
            if (a.FrontRank < b.FrontRank)
            {
                return true;
            }

            if (a.FrontRank > b.FrontRank)
            {
                return false;
            }

            return a.CrowdingDistance > b.CrowdingDistance;
        }
    }
}
