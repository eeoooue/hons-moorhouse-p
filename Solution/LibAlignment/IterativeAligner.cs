using LibAlignment.Helpers;
using LibBioInfo;
using LibBioInfo.LegacyAlignmentModifiers;
using LibScoring;
using System;
using System.Text;

namespace LibAlignment
{
    public abstract class IterativeAligner : IIterativeAligner
    {
        public IFitnessFunction Objective { get; protected set; }

        public Alignment CurrentAlignment { get { return CurrentBest.Alignment; } }

        public int IterationsCompleted { get; protected set; } = 0;

        public int IterationsLimit { get; set; } = 0;

        public double AlignmentScore { get { return CurrentBest.Score; } }

        protected ScoredAlignment CurrentBest = null!;

        public IterativeAligner(IFitnessFunction objective, int iterations)
        {
            Objective = objective;
            IterationsLimit = iterations;
        }

        public Alignment AlignSequences(List<BioSequence> sequences)
        {
            Initialize(sequences);
            while (IterationsCompleted < IterationsLimit)
            {
                Iterate();
            }
            return CurrentAlignment!;
        }

        public abstract void Initialize(List<BioSequence> sequences);

        public abstract void InitializeForRefinement(Alignment alignment);

        public abstract void PerformIteration();

        public void Iterate()
        {
            PerformIteration();
            IterationsCompleted++;
        }

        public void CheckNewBest(ScoredAlignment candidate)
        {
            if (candidate.Score > CurrentBest.Score)
            {
                CurrentBest = candidate.GetCopy();
            }
        }

        public double ScoreAlignment(Alignment alignment)
        {
            return Objective.GetFitness(alignment.GetCharacterMatrix());
        }

        public abstract string GetName();

        public ScoredAlignment GetRandomScoredAlignment(List<BioSequence> sequences)
        {
            Alignment alignment = GetRandomAlignment(sequences);
            return GetScoredAlignment(alignment);
        }

        public Alignment GetRandomAlignment(List<BioSequence> sequences)
        {
            ILegacyAlignmentModifier randomizer = new AlignmentRandomizer();
            Alignment alignment = new Alignment(sequences);
            randomizer.ModifyAlignment(alignment);
            return alignment;
        }

        public ScoredAlignment GetScoredAlignment(Alignment alignment)
        {
            double score = ScoreAlignment(alignment);
            return new ScoredAlignment(alignment, score);
        }
    }
}
