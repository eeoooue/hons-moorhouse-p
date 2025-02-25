using LibBioInfo;
using LibModification.Mechanisms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.CrossoverOperators
{
    public class RowBasedCrossoverOperator : ICrossoverOperator
    {
        public List<Alignment> CreateAlignmentChildren(Alignment a, Alignment b)
        {
            bool[] mapping = ConstructRandomColumnMapping(a.Height);
            return VerticalCrossover.ProduceChildrenFromMapping(a, b, mapping);
        }

        public bool[] ConstructRandomColumnMapping(int n)
        {
            bool[] result = new bool[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = Randomizer.CoinFlip();
            }

            return result;
        }
    }
}
