using LibAlignment;
using LibBioInfo;
using MAli.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.AlignmentConfigs
{
    public abstract class BaseAlignmentConfig
    {
        public abstract IterativeAligner CreateAligner();

        public IterativeAligner InitialiseAligner(Alignment alignment, AlignmentRequest instructions)
        {
            IterativeAligner aligner = CreateAligner();

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
