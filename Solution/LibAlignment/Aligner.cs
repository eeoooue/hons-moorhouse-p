using LibBioInfo;
using LibScoring;

namespace LibAlignment
{
    public abstract class Aligner
    {
        private IObjectiveFunction Objective;
        public Alignment? CurrentAlignment = null;

        public int IterationsCompleted { get; protected set; } = 0;

        public int IterationsLimit { get; set; } = 0;

        public double AlignmentScore { get; protected set; } = 0;

        public Aligner(IObjectiveFunction objective, int iterations)
        {
            Objective = objective;
            IterationsLimit = iterations;
        }

        public abstract Alignment AlignSequences(List<BioSequence> sequences);

        public abstract void Initialize(List<BioSequence> sequences);

        public abstract void Iterate();

        public double ScoreAlignment(Alignment alignment)
        {
            return Objective.ScoreAlignment(alignment);
        }
    }
}
