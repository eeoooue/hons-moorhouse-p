using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo
{
    public class PairwiseScoringScheme
    {
        public int ResidueMatch { get; }

        public int ResidueMismatch { get; }

        public int ResidueWithGap { get; }

        public int GapWithGap { get; } = 0;

        public PairwiseScoringScheme(int match = 1, int mismatch = -1, int gap = -2)
        {
            ResidueMatch = match;
            ResidueMismatch = mismatch;
            ResidueWithGap = gap;
        }
    }
}
