using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibParetoAlignment
{
    public class DominationDecorator
    {
        public int DominationCounter = 0;

        public List<DominationDecorator> DominatedSolutions = new List<DominationDecorator>();

        public bool AddedToFront = false;

        public TradeoffAlignment Tradeoff;

        public DominationDecorator(TradeoffAlignment tradeoff)
        {
            Tradeoff = tradeoff;
        }

        public void SetFrontRank(int rank)
        {
            Tradeoff.FrontRank = rank;
        }
    }
}
