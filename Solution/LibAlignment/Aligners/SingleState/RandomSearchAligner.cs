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
        public RandomSearchAligner(IObjectiveFunction objective, int iterations) : base(objective, iterations)
        {

        }

        public override string GetName()
        {
            return $"Random Search Aligner";
        }

        public override void PerformIteration()
        {
            Alignment alignment = GetRandomAlignment(S.Alignment.Sequences);
            ScoredAlignment candidate = GetScoredAlignment(alignment);
            ContestS(candidate);
        }
    }
}
