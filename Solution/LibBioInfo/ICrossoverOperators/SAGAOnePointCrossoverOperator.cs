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
        public RowBasedCrossoverOperator RowBasedOperator = new RowBasedCrossoverOperator();

        // an attempt to reproduce the One-Point Crossover operation described in SAGA (Notredame & Higgins, 1996)

        private AlignmentStateHelper StateHelper = new AlignmentStateHelper();

        public List<Alignment> CreateAlignmentChildren(Alignment a, Alignment b)
        {
            if (Randomizer.CoinFlip())
            {
                int i = Randomizer.Random.Next(2, a.Width);
                return RowBasedOperator.CrossoverAtPosition(a, b, i);
            }
            else
            {
                int i = Randomizer.Random.Next(2, b.Width);
                return RowBasedOperator.CrossoverAtPosition(b, a, i);
            }
        }

        public List<Alignment> CrossoverAtPosition(Alignment a, Alignment b, int position)
        {
            return RowBasedOperator.CrossoverAtPosition(b, a, position);
        }
    }
}
