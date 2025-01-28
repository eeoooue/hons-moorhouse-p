using LibBioInfo;
using LibModification;
using LibModification.AlignmentModifiers;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAlignment.Aligners.SingleState
{
    public class HillClimbWithRandomRestartsAligner : SingleStateAligner
    {
        public IAlignmentModifier Modifier = new SwapOperator();
        public int ResetPoint = 0;
        protected ScoredAlignment S = null!;


        public HillClimbWithRandomRestartsAligner(IFitnessFunction objective, int iterations) : base(objective, iterations)
        {

        }

        public override string GetName()
        {
            return $"Hill Climb w/ Random Restarts : (next restart @ {ResetPoint})";
        }

        public override void AdditionalSetup()
        {
            S = CurrentBest.GetCopy();
            MarkUpcomingResetPoint();
        }

        public void ContestS(ScoredAlignment candidate)
        {
            if (candidate.Score > S.Score)
            {
                S = candidate;
                CheckNewBest(S);
            }
        }

        public void MarkUpcomingResetPoint()
        {
            int range = IterationsLimit / 5;
            int roll = Randomizer.Random.Next(1, range + 1);
            ResetPoint = IterationsCompleted + roll;
        }

        public override void PerformIteration()
        {
            if (IterationsCompleted == ResetPoint)
            {
                S = GetRandomScoredAlignment(S.Alignment.Sequences);
                MarkUpcomingResetPoint();
            }

            Alignment r = S.Alignment.GetCopy();
            Modifier.ModifyAlignment(r);

            ScoredAlignment candidate = GetScoredAlignment(r);
            ContestS(candidate);
        }
    }
}
