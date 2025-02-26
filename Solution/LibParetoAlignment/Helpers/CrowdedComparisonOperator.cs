using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibParetoAlignment.Helpers
{
    public class CrowdedComparisonOperator
    {
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

            return a.CrowdingDistance < b.CrowdingDistance;
        }
    }
}
