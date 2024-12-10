using LibBioInfo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.ICrossoverOperators
{
    public class SAGAOnePointCrossoverOperator : ICrossoverOperator
    {
        public ColBasedCrossoverOperator ColBasedOperator = new ColBasedCrossoverOperator();

        // an attempt to reproduce the One-Point Crossover operation described in SAGA (Notredame & Higgins, 1996)

        private AlignmentStateHelper StateHelper = new AlignmentStateHelper();

        public List<Alignment> CreateAlignmentChildren(Alignment a, Alignment b)
        {
            if (Randomizer.CoinFlip())
            {
                int i = Randomizer.Random.Next(2, a.Width);
                return ColBasedOperator.CrossoverAtPosition(a, b, i);
            }
            else
            {
                int i = Randomizer.Random.Next(2, b.Width);
                return ColBasedOperator.CrossoverAtPosition(b, a, i);
            }
        }

        public List<Alignment> CrossoverAtPosition(Alignment a, Alignment b, int position)
        {
            return ColBasedOperator.CrossoverAtPosition(b, a, position);
        }
    }
}
