using LibAlignment.Helpers;
using LibBioInfo;
using LibScoring;
using System;
using System.Text;

namespace LibAlignment
{
    public abstract class IterativeAligner : IIterativeAligner
    {
        public IObjectiveFunction Objective { get; protected set; }

        public Alignment? CurrentAlignment { get; protected set; }

        public int IterationsCompleted { get; protected set; } = 0;

        public int IterationsLimit { get; set; } = 0;

        public double AlignmentScore { get; protected set; } = 0;

        public IterativeAligner(IObjectiveFunction objective, int iterations)
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
            if (candidate.Score > AlignmentScore)
            {
                CurrentAlignment = candidate.Alignment.GetCopy();
                AlignmentScore = candidate.Score;
            }
        }

        public double ScoreAlignment(Alignment alignment)
        {
            return Objective.ScoreAlignment(alignment);
        }

        public abstract string GetName();
    }
}
