using LibBioInfo;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAlignment
{
    public abstract class ParetoAligner : IIterativeAligner
    {
        public IFitnessFunction Objective { get; protected set; }

        public Alignment CurrentAlignment { get { return CurrentBest.Alignment; } }

        public int IterationsCompleted { get; protected set; } = 0;

        public int IterationsLimit { get; set; } = 0;

        protected ScoredAlignment CurrentBest = null!; // NEW SCORING ABSTRACTION?

        protected Queue<ScoredAlignment> Archive = new Queue<ScoredAlignment>();

        public Alignment AlignSequences(List<BioSequence> sequences)
        {
            throw new NotImplementedException();
        }

        public void Initialize(List<BioSequence> sequences)
        {
            throw new NotImplementedException();
        }

        public void InitializeForRefinement(Alignment alignment)
        {
            throw new NotImplementedException();
        }

        public void Iterate()
        {
            throw new NotImplementedException();
        }
    }
}
