using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsUnitSuite.HarnessTools
{
    internal class StateEquality
    {

        public bool RowsMatch(bool[] expected, bool[] actual)
        {
            if (expected.Length != actual.Length)
            {
                return false;
            }

            for(int i=0; i<expected.Length; i++)
            {
                if (expected[i] != actual[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
