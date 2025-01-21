using LibBioInfo;
using LibBioInfo.IAlignmentModifiers;
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
        public IAlignmentModifier PerturbModifier = new MultiRowStochasticSwapOperator();
        public IAlignmentModifier TweakModifier = new MultiRowStochasticSwapOperator();
        List<BioSequence> Sequences = new List<BioSequence>();

        public int ResetPoint = 0;

        public ScoredAlignment S = null!;

        public ScoredAlignment HomeBase = null!;

        public IteratedLocalSearchAligner(IObjectiveFunction objective, int iterations) : base(objective, iterations)
        {
        }


        public override string GetName()
        {
            return $"Iterated Local Search : (next restart @ {ResetPoint}, home base score = {HomeBase.Score})";
        }

        public override void Initialize(List<BioSequence> sequences)
        {
            Sequences = sequences;
            Alignment initialAlignment = GetRandomAlignment();
            S = GetScoredAlignment(initialAlignment);
            HomeBase = S.GetCopy();
            CurrentAlignment = S.Alignment;
            AlignmentScore = S.Score;
        }

        public ScoredAlignment GetPerturbationOfH()
        {
            Alignment homeCopy = HomeBase.Alignment.GetCopy();
            PerturbModifier.ModifyAlignment(homeCopy);
            return GetScoredAlignment(homeCopy);
        }

        public Alignment GetRandomAlignment()
        {
            IAlignmentModifier randomizer = new AlignmentRandomizer();
            Alignment alignment = new Alignment(Sequences);
            randomizer.ModifyAlignment(alignment);
            return alignment;
        }
        public ScoredAlignment GetScoredAlignment(Alignment alignment)
        {
            double score = ScoreAlignment(alignment);
            return new ScoredAlignment(alignment, score);
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

        public void ContestS(ScoredAlignment candidate)
        {
            if (candidate.Score > S.Score)
            {
                S = candidate;
                CheckNewBest(S);
            }
        }
    }
}
