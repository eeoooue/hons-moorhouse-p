using LibBioInfo;
using LibScoring;

namespace LibAlignment
{
    public abstract class Aligner
    {
        public IObjectiveFunction Objective;
        public Alignment? CurrentAlignment = null;

        public int IterationsCompleted { get; protected set; } = 0;

        public int IterationsLimit { get; private set; } = 0;

        public float AlignmentScore { get; protected set; } = 0;

        public Aligner(IObjectiveFunction objective, int iterations)
        {
            Objective = objective;
            IterationsLimit = iterations;
        }

    }
}
