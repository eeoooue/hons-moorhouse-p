using LibBioInfo;
using LibModification.AlignmentInitializers;
using LibModification;
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
        private IAlignmentInitializer RandomInitializer = new RandomizationInitializer();

        public RandomSearchAligner(IFitnessFunction objective, int iterations) : base(objective, iterations)
        {

        }

        public override string GetName()
        {
            return $"Random Search Aligner";
        }

        public override void PerformIteration()
        {
            Alignment alignment = RandomInitializer.CreateInitialAlignment(CurrentAlignment.Sequences);
            ScoredAlignment candidate = GetScoredAlignment(alignment);
            CheckNewBest(candidate);
        }
    }
}
