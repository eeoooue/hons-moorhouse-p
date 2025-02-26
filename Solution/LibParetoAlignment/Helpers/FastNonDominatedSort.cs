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
            List<DominationDecorator> decorated = DecorateTradeoffs(tradeoffs);
            List<DominationDecorator> frontSeries = new List<DominationDecorator>();
            ExtractFronts(decorated, frontSeries);

            List<TradeoffAlignment> result = new List<TradeoffAlignment>();
            foreach(DominationDecorator wrappedTradeoff in frontSeries)
            {
                result.Add(wrappedTradeoff.Tradeoff);
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

        public void ExtractFronts(List<DominationDecorator> tradeoffs, List<DominationDecorator> frontSeries)
        {
            List <DominationDecorator> currentFront = new List<DominationDecorator>();

            foreach (DominationDecorator p in tradeoffs)
            {
                foreach(DominationDecorator q in tradeoffs)
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

        public void ExtractNextFront(List<DominationDecorator> previousFront, List<DominationDecorator> frontSeries, int frontRank)
        {
            if (previousFront.Count == 0)
            {
                return;
            }

            List<DominationDecorator> currentFront = new List<DominationDecorator>();

            foreach (DominationDecorator p in previousFront)
            {
                foreach (DominationDecorator q in p.DominatedSolutions)
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

        public List<DominationDecorator> DecorateTradeoffs(List<TradeoffAlignment> tradeoffs)
        {
            List<DominationDecorator> result = new List<DominationDecorator>();
            foreach(TradeoffAlignment tradeoff in tradeoffs)
            {
                DominationDecorator decorated = new DominationDecorator(tradeoff);
                result.Add(decorated);
            }

            return result;
        }
    }
}
