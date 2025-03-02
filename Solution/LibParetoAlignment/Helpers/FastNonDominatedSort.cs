using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibParetoAlignment.Helpers
{
    public class FastNonDominatedSort
    {
        private ParetoHelper ParetoHelper = new ParetoHelper();

        public List<TradeoffAlignment> SortTradeoffs(List<TradeoffAlignment> tradeoffs)
        {
            ResetDominationState(tradeoffs);

            List<TradeoffAlignment> frontSeries = new List<TradeoffAlignment>();
            ExtractFronts(tradeoffs, frontSeries);

            List<TradeoffAlignment> result = new List<TradeoffAlignment>();
            foreach(TradeoffAlignment tradeoff in frontSeries)
            {
                result.Add(tradeoff);
            }

            if (result.Count < tradeoffs.Count)
            {
                throw new Exception("too few solutions sorted");
            }

            if (result.Count > tradeoffs.Count)
            {
                throw new Exception("duplicate solutions in the sorted series");
            }

            return result;
        }

        public void ResetDominationState(List<TradeoffAlignment> tradeoffs)
        {
            foreach(TradeoffAlignment alignment in tradeoffs)
            {
                alignment.ResetDominationVariables();
            }
        }


        public void ExtractFronts(List<TradeoffAlignment> tradeoffs, List<TradeoffAlignment> frontSeries)
        {
            List <TradeoffAlignment> currentFront = new List<TradeoffAlignment>();

            foreach (TradeoffAlignment p in tradeoffs)
            {
                foreach(TradeoffAlignment q in tradeoffs)
                {
                    bool pDominatesQ = ParetoHelper.ADominatesB(p, q);
                    if (pDominatesQ)
                    {
                        p.DominatedSolutions.Add(q);
                    }
                    else if (ParetoHelper.ADominatesB(q, p))
                    {
                        p.DominationCounter += 1;
                    }
                }

                if (p.DominationCounter == 0)
                {
                    p.SetFrontRank(1);
                    frontSeries.Add(p);
                    currentFront.Add(p);
                }
            }

            ExtractNextFront(currentFront, frontSeries, 2);
        }

        public void ExtractNextFront(List<TradeoffAlignment> previousFront, List<TradeoffAlignment> frontSeries, int frontRank)
        {
            if (previousFront.Count == 0)
            {
                return;
            }

            List<TradeoffAlignment> currentFront = new List<TradeoffAlignment>();

            foreach (TradeoffAlignment p in previousFront)
            {
                foreach (TradeoffAlignment q in p.DominatedSolutions)
                {
                    q.DominationCounter -= 1;
                    if (q.DominationCounter == 0)
                    {
                        q.SetFrontRank(frontRank);
                        frontSeries.Add(q);
                        currentFront.Add(q);
                    }
                }
            }

            ExtractNextFront(currentFront, frontSeries, frontRank+1);
        }
    }
}
