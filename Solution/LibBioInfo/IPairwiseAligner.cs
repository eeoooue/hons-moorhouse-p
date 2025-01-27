using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo
{
    public interface IPairwiseAligner
    {
        public char[,] AlignSequences(string sequenceA, string sequenceB);
    }
}
