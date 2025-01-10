using LibBioInfo;
using LibBioInfo.IAlignmentModifiers;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAlignment.Aligners
{
    public class IteratedLocalSearchAligner : Aligner
    {
        IAlignmentModifier Modifier = new SwapOperator();
        List<BioSequence> Sequences = new List<BioSequence>();

        public int ResetPoint = 0;

        public ScoredAlignment S = null!;

        public IteratedLocalSearchAligner(IObjectiveFunction objective, int iterations) : base(objective, iterations)
        {
        }

        public override Alignment AlignSequences(List<BioSequence> sequences)
        {
            Initialize(sequences);
            while (IterationsCompleted < IterationsLimit)
            {
                Iterate();
                IterationsCompleted++;
                CheckShowDebuggingInfo();
            }

            return CurrentAlignment!;
        }

        public override string GetName()
        {
            return $"Hill Climb w/ Random Restarts : (next restart @ {ResetPoint})";
        }

        public override void Initialize(List<BioSequence> sequences)
        {
            Sequences = sequences;
            S = GetRandomScoredAlignment();
            CurrentAlignment = S.Alignment;
            AlignmentScore = S.Score;
        }

        public ScoredAlignment GetRandomScoredAlignment()
        {
            Alignment alignment = GetRandomAlignment();
            return GetScoredAlignment(alignment);
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
            int range = IterationsLimit / 5;
            int roll = Randomizer.Random.Next(1, range + 1);
            ResetPoint = IterationsCompleted + roll;
        }

        public override void Iterate()
        {
            if (IterationsCompleted == ResetPoint)
            {
                S = GetRandomScoredAlignment();
                MarkUpcomingResetPoint();
            }

            Alignment r = S.Alignment.GetCopy();
            Modifier.ModifyAlignment(r);

            ScoredAlignment candidate = GetScoredAlignment(r);
            ContestS(candidate);
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
