using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.ICrossoverOperators
{
    internal class OnePointCrossoverOperator : ICrossoverOperator
    {

        // based on One-Point Crossover operation described in SAGA (Notredame & Higgins, 1996)

        public Alignment CreateChildAlignment(Alignment a, Alignment b)
        {

            // the alignment A is cut at a randomly chosen position

            // complement segments are taken from B such that the resulting child is a valid state

            int i = Randomizer.Random.Next(a.Width);
            return CrossoverAtPosition(a, b, i);
        }

        public Alignment CrossoverAtPosition(Alignment a, Alignment b, int i)
        {






            throw new NotImplementedException();
        }
    }
}
