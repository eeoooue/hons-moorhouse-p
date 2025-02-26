using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibParetoAlignment.Helpers
{
    public class CrowdingDistanceAssignment
    {
        public void AssignDistances(List<TradeoffAlignment> tradeoffs)
        {
            List<string> objectives = tradeoffs[0].Scores.Keys.ToList();

            SetAllDistancesToZero(tradeoffs);
            foreach (string objective in objectives)
            {
                SortTradeoffsByObjectiveInAscendingOrder(tradeoffs, objective);
                AssignDistancesToSortedTradeoffs(tradeoffs, objective, 1.0);
            }
        }

        public void SetAllDistancesToZero(List<TradeoffAlignment> tradeoffs)
        {
            foreach(TradeoffAlignment tradeoff in tradeoffs)
            {
                tradeoff.CrowdingDistance = 0;
            }
        }

        public void SortTradeoffsByObjectiveInAscendingOrder(List<TradeoffAlignment> tradeoffs, string objective)
        {
            int n = tradeoffs.Count;
            for (int i = 0; i < n - 1; i++)
            {
                int currentMinIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (tradeoffs[j].Scores[objective] < tradeoffs[currentMinIndex].Scores[objective])
                    {
                        currentMinIndex = j;
                    }
                }

                TradeoffAlignment temp = tradeoffs[i];
                tradeoffs[i] = tradeoffs[currentMinIndex];
                tradeoffs[currentMinIndex] = temp;
            }
        }


        public void AssignDistancesToSortedTradeoffs(List<TradeoffAlignment> tradeoffs, string objective, double scoreRange)
        {
            int n = tradeoffs.Count;
            tradeoffs[0].CrowdingDistance = double.MaxValue;
            tradeoffs[n-1].CrowdingDistance = double.MaxValue;

            for (int i=1; i<n-1; i++)
            {
                TradeoffAlignment current = tradeoffs[i];
                double scoreLeft = tradeoffs[i - 1].Scores[objective];
                double scoreRight = tradeoffs[i + 1].Scores[objective];
                double contribution = (scoreLeft - scoreRight) / scoreRange;

                // TODO: review more efficient way to handle overflow due to double.MaxValue
                current.CrowdingDistance = Math.Max(current.CrowdingDistance + contribution, current.CrowdingDistance); 
            }
        }
    }
}
