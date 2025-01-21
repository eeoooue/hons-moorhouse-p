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
    public abstract class SingleStateAligner : IterativeAligner
    {
        protected ScoredAlignment S = null!;

        protected SingleStateAligner(IObjectiveFunction objective, int iterations) : base(objective, iterations)
        {

        }

        public void ContestS(ScoredAlignment candidate)
        {
            if (candidate.Score > S.Score)
            {
                S = candidate;
                CheckNewBest(S);
            }
        }

        public override void Initialize(List<BioSequence> sequences)
        {
            S = GetRandomScoredAlignment(sequences);
            InitialiseAroundState(S);
        }

        public override void InitializeForRefinement(Alignment alignment)
        {
            S = GetScoredAlignment(alignment);
            InitialiseAroundState(S);
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
