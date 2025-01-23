using LibBioInfo;
using LibBioInfo.LegacyAlignmentModifiers;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAlignment.Aligners.SingleState
{
    public class IteratedLocalSearchAligner : SingleStateAligner
    {
        public ILegacyAlignmentModifier PerturbModifier = new MultiRowStochasticSwapOperator();
        public ILegacyAlignmentModifier TweakModifier = new MultiRowStochasticSwapOperator();

        public int ResetPoint = 0;
        public ScoredAlignment HomeBase = null!;
        protected ScoredAlignment S = null!;

        public IteratedLocalSearchAligner(IFitnessFunction objective, int iterations) : base(objective, iterations)
        {

        }

        public override string GetName()
        {
            return $"Iterated Local Search : (next restart @ {ResetPoint}, home base score = {HomeBase.Score})";
        }

        public override void AdditionalSetup()
        {
            S = CurrentBest.GetCopy();
            HomeBase = S.GetCopy();
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

        public ScoredAlignment GetPerturbationOfH()
        {
            Alignment homeCopy = HomeBase.Alignment.GetCopy();
            PerturbModifier.ModifyAlignment(homeCopy);
            return GetScoredAlignment(homeCopy);
        }

        public void MarkUpcomingResetPoint()
        {
            int range = IterationsLimit / 10;
            int roll = Randomizer.Random.Next(1, range + 1);
            ResetPoint = IterationsCompleted + roll;
        }

        public override void PerformIteration()
        {
            if (IterationsCompleted == ResetPoint)
            {
                ContestHomeBase(S);
                S = GetPerturbationOfH();
                MarkUpcomingResetPoint();
            }

            Alignment r = S.Alignment.GetCopy();
            TweakModifier.ModifyAlignment(r);

            ScoredAlignment candidate = GetScoredAlignment(r);
            ContestS(candidate);
        }

        public void ContestHomeBase(ScoredAlignment candidate)
        {
            if (candidate.Score > HomeBase.Score)
            {
                HomeBase = candidate;
            }
        }
    }
}
