using LibBioInfo;
using LibBioInfo.INeighbourhoodFinders;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAlignment.Aligners.SingleState
{
    public class NaiveHillClimbAligner : SingleStateAligner
    {
        public INeighbourhoodFinder NeighbourhoodFinder = new SwapBasedNeighbourhoodFinder();

        public NaiveHillClimbAligner(IFitnessFunction objective, int iterations) : base(objective, iterations)
        {
        }

        public override string GetName()
        {
            return $"NaiveHillClimbAligner";
        }

        public override void PerformIteration()
        {
            foreach (Alignment candidate in GetNeighbouringAlignments(CurrentAlignment!))
            {
                ScoredAlignment scored = GetScoredAlignment(candidate);
                CheckNewBest(scored);
            }
        }

        public List<Alignment> GetNeighbouringAlignments(Alignment alignment)
        {
            throw new NotImplementedException();

            //List<Alignment> result = new List<Alignment>();

            //List<bool[,]> neighbouringStates = NeighbourhoodFinder.FindNeighbours(alignment.State);

            //foreach (bool[,] state in neighbouringStates)
            //{
            //    Alignment neighbour = alignment.GetCopy();
            //    neighbour.SetState(state);
            //    result.Add(neighbour);
            //}

            //return result;
        }
    }
}
