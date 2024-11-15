using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring.ScoringMatrices
{
    public class IdentityMatrix : IScoringMatrix
    {
        public int ScorePair(char a, char b)
        {
            if (a == b)
            {
                return 1;
            }

            return 0;
        }
    }
}
