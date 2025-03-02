using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibParetoAlignment.Helpers
{
    internal class DuplicationChecker
    {

        public static bool SolutionNotInList(Alignment solution, List<TradeoffAlignment> list)
        {
            foreach (TradeoffAlignment tradeoff in list)
            {
                if (SolutionsAreIdentical(solution, tradeoff.Alignment))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool SolutionNotInList(Alignment solution, List<Alignment> list)
        {
            foreach(Alignment alignment in list)
            {
                if (SolutionsAreIdentical(solution, alignment))
                {
                    return false;
                }
            }

            return true;
        }


        public static bool SolutionsAreIdentical(Alignment a, Alignment b)
        {
            if (a.Width != b.Width)
            {
                return false;
            }

            for(int i=0; i<a.Height; i++)
            {
                if (!RowsMatch(a, b, i))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool RowsMatch(Alignment a, Alignment b, int i)
        {
            for(int j=0; j<a.Width; j++)
            {
                char x = a.CharacterMatrix[i, j];
                char y = a.CharacterMatrix[i, j];
                if (x != y)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
