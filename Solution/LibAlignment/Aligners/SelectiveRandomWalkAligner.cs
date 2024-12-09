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
    public sealed class SelectiveRandomWalkAligner : Aligner
    {
        public SelectiveRandomWalkAligner(IObjectiveFunction objective, int iterations) : base(objective, iterations)
        {

        }

        public override string GetName()
        {
            return $"SelectiveRandomWalkAligner";
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
            IAlignmentModifier shifter = new MultiRowStochasticGapShifter();
            Alignment candidate = CurrentAlignment!.GetCopy();
            shifter.ModifyAlignment(candidate);
            AcceptIfImprovement(candidate);
        }
    }
}
