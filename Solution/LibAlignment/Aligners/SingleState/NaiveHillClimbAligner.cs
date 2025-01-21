using LibBioInfo;
using LibBioInfo.IAlignmentModifiers;
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
        public NaiveHillClimbAligner(IObjectiveFunction objective, int iterations) : base(objective, iterations)
        {
        }

        public override string GetName()
        {
            return $"NaiveHillClimbAligner";
        }

        public override void Initialize(List<BioSequence> sequences)
        {
            CurrentAlignment = new Alignment(sequences);
            IAlignmentModifier randomizer = new AlignmentRandomizer();
            randomizer.ModifyAlignment(CurrentAlignment);
            IterationsCompleted = 0;
            AlignmentScore = ScoreAlignment(CurrentAlignment);
        }

        public void AcceptIfImprovement(Alignment candidate)
        {
            double score = ScoreAlignment(candidate);
            if (score > AlignmentScore)
            {
                CurrentAlignment = candidate;
                AlignmentScore = score;
            }
        }

        public override void Iterate()
        {
            foreach (Alignment candidate in GetNeighbouringAlignments(CurrentAlignment!))
            {
                AcceptIfImprovement(candidate);
            }
            MarkIterationComplete();
        }

        public List<Alignment> GetNeighbouringAlignments(Alignment alignment)
        {
            List<Alignment> result = new List<Alignment>();
            INeighbourhoodFinder finder = new SwapBasedNeighbourhoodFinder();

            List<bool[,]> neighbouringStates = finder.FindNeighbours(alignment.State);

            foreach (bool[,] state in neighbouringStates)
            {
                Alignment neighbour = alignment.GetCopy();
                neighbour.SetState(state);
                result.Add(neighbour);
            }

            return result;
        }
    }
}
