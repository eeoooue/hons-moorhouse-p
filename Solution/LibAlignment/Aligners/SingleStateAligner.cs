using LibBioInfo;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAlignment.Aligners
{
    public abstract class SingleStateAligner : IterativeAligner
    {
        protected SingleStateAligner(IFitnessFunction objective, int iterations) : base(objective, iterations)
        {

        }

        public override void Initialize(List<BioSequence> sequences)
        {
            ScoredAlignment scoredAlignment = GetRandomScoredAlignment(sequences);
            InitialiseAroundState(scoredAlignment);
        }

        public override void InitializeForRefinement(Alignment alignment)
        {
            ScoredAlignment scoredAlignment = GetScoredAlignment(alignment);
            InitialiseAroundState(scoredAlignment);
        }

        public void InitialiseAroundState(ScoredAlignment alignment)
        {
            IterationsCompleted = 0;
            CurrentBest = alignment;
            AdditionalSetup();
        }

        public virtual void AdditionalSetup() { }
    }
}
