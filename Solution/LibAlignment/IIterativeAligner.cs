using LibBioInfo;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAlignment
{
    public interface IIterativeAligner
    {
        public IObjectiveFunction Objective { get; }

        public Alignment? CurrentAlignment { get; }

        public int IterationsCompleted { get; } 

        public int IterationsLimit { get; set; } 

        public double AlignmentScore { get; }

        public Alignment AlignSequences(List<BioSequence> sequences);

        public void Initialize(List<BioSequence> sequences);

        public void InitializeForRefinement(Alignment alignment);

        public void Iterate();
    }
}
