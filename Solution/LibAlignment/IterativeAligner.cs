using LibBioInfo;
using LibScoring;
using System;
using System.Text;
using LibModification.AlignmentModifiers;
using LibModification;
using LibModification.AlignmentInitializers;

namespace LibAlignment
{
    public abstract class IterativeAligner
    {
        public IAlignmentInitializer Initializer = new RelativeOffsetInitializer();

        public IFitnessFunction Objective { get; set; }

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

        protected void CheckNewBest(ScoredAlignment candidate)
        {
            if (candidate.Score > CurrentBest.Score)
            {
                CurrentBest = candidate.GetCopy();
            }
        }

        protected double ScoreAlignment(Alignment alignment)
        {
            return Objective.GetFitness(alignment.CharacterMatrix);
        }

        public abstract string GetName();

        protected ScoredAlignment GetScoredAlignment(Alignment alignment)
        {
            double score = ScoreAlignment(alignment);
            return new ScoredAlignment(alignment, score);
        }
    }
}
