using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.PairwiseAligners
{
    public class NeedlemanWunschAligner : IPairwiseAligner
    {



        public char[,] AlignSequences(string sequenceA, string sequenceB)
        {
            int m = sequenceA.Length;
            int n = sequenceB.Length;

            char[,] result = new char[m, n];

            return result;
        }






    }
}
