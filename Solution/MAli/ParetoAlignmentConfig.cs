using LibAlignment;
using LibBioInfo;
using LibParetoAlignment;
using MAli.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli
{
    public abstract class ParetoAlignmentConfig
    {
        public abstract ParetoIterativeAligner CreateAligner();

        public ParetoIterativeAligner InitialiseAligner(Alignment alignment, AlignmentRequest instructions)
        {
            ParetoIterativeAligner aligner = CreateAligner();

            if (instructions.IterationsLimit > 0)
            {
                aligner.IterationsLimit = instructions.IterationsLimit;
            }
            else
            {
                aligner.IterationsLimit = instructions.IterationsLimit;
            }

            if (instructions.RefineOnly)
            {
                aligner.InitializeForRefinement(alignment);
            }
            else
            {
                aligner.Initialize(alignment.Sequences);
            }

            return aligner;
        }
    }
}
