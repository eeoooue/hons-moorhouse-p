using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring.ScoringMatrices
{
    public class IdentityMatrix : IScoringMatrix
    {
        private Bioinformatics Bioinformatics = new Bioinformatics();

        public int ScorePair(char a, char b)
        {
            if (Bioinformatics.IsGapChar(a) || Bioinformatics.IsGapChar(b))
            {
                return 0;
            }

            if (a == b)
            {
                return 1;
            }

            return 0;
        }
    }
}
