using LibBioInfo;
using LibScoring;

namespace LibAlignment
{
    public abstract class Aligner
    {
        public IObjectiveFunction Objective;
        public Alignment CurrentAlignment;
    }
}
