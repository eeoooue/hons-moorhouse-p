using LibBioInfo;
using LibModification.AlignmentInitializers;
using LibModification;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibSimilarity;

namespace LibAlignment.Aligners
{
    public abstract class SingleStateAligner : IterativeAligner
    {
        protected SingleStateAligner(IFitnessFunction objective, int iterations) : base(objective, iterations)
        {

        }

        public override void Initialize(List<BioSequence> sequences)
        {
            Alignment alignment = Initializer.CreateInitialAlignment(sequences);
            SimilarityGuide.SetSequences(sequences);
            ScoredAlignment scoredAlignment = GetScoredAlignment(alignment);
            InitialiseAroundState(scoredAlignment);
        }

        public override void InitializeForRefinement(Alignment alignment)
        {
            SimilarityGuide.SetSequences(alignment.Sequences);
            ScoredAlignment scoredAlignment = GetScoredAlignment(alignment);
            InitialiseAroundState(scoredAlignment);
        }

        public void InitialiseAroundState(ScoredAlignment alignment)
        {
            IterationsCompleted = 0;
            CurrentBest = alignment;
            AdditionalSetup();
        }

        protected virtual void AdditionalSetup() { }
    }
}
