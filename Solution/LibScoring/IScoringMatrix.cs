using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring
{
    public interface IScoringMatrix
    {
        public abstract int ScorePair(char a, char b);

        public abstract List<char> GetResidues();
    }
}
