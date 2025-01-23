using LibBioInfo;
using LibBioInfo.AlignmentModifiers;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAlignment.Aligners.SingleState
{
    public sealed class SelectiveRandomWalkAligner : SingleStateAligner
    {
        public IAlignmentModifier Modifier = new MultiRowStochasticGapShifter();

        public SelectiveRandomWalkAligner(IFitnessFunction objective, int iterations) : base(objective, iterations)
        {

        }

        public override string GetName()
        {
            return $"SelectiveRandomWalkAligner";
        }

        public override void PerformIteration()
        {
            Alignment candidate = CurrentAlignment.GetCopy();
            Modifier.ModifyAlignment(candidate);
            ScoredAlignment scoredAlignment = GetScoredAlignment(candidate);
            CheckNewBest(scoredAlignment);
        }
    }
}
