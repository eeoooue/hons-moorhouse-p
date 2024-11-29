using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsUnitSuite.HarnessTools
{
    internal class AlignmentStateConverter
    {
        public bool[,] ConvertToAlignmentState(List<string> state)
        {
            int m = state.Count;
            int n = state[0].Length;

            bool[,] result = new bool[m, n];

            for(int i=0; i<m; i++)
            {
                for(int j=0; j<n; j++)
                {
                    char character = state[i][j];
                    result[i, j] = character == '-' ? true : false;
                }
            }

            return result;
        }
    }
}
