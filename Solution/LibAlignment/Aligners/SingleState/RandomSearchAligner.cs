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
    public class RandomSearchAligner : SingleStateAligner
    {
        IAlignmentModifier Modifier = new AlignmentRandomizer(); // this should change width too?

        List<BioSequence> Sequences = new List<BioSequence>();

        public RandomSearchAligner(IObjectiveFunction objective, int iterations) : base(objective, iterations)
        {

        }


        public override string GetName()
        {
            return $"Random Search Aligner";
        }

        public override void Initialize(List<BioSequence> sequences)
        {
            Sequences = sequences;
            Alignment alignment = GetRandomAlignment();
            ScoredAlignment candidate = GetScoredAlignment(alignment);
            CurrentAlignment = candidate.Alignment;
            AlignmentScore = candidate.Score;
        }

        public Alignment GetRandomAlignment()
        {
            Alignment alignment = new Alignment(Sequences);
            Modifier.ModifyAlignment(alignment);
            return alignment;
        }
        public ScoredAlignment GetScoredAlignment(Alignment alignment)
        {
            double score = ScoreAlignment(alignment);
            return new ScoredAlignment(alignment, score);
        }

        public override void PerformIteration()
        {
            Alignment alignment = GetRandomAlignment();
            ScoredAlignment candidate = GetScoredAlignment(alignment);
            CheckNewBest(candidate);
        }
    }
}
