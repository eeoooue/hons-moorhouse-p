using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsHarness.Tools
{
    public class StateEquality
    {

        public bool StatesMatch(bool[,] expected, bool[,] actual)
        {
            int m = expected.GetLength(0);
            int n = expected.GetLength(1);

            if (expected.GetLength(0) != actual.GetLength(0))
            {
                return false;
            }

            if (expected.GetLength(1) != actual.GetLength(1))
            {
                return false;
            }

            for (int i = 0; i < m; i++)
            {
                bool[] expectedRow = ExtractRow(expected, i);
                bool[] actualRow = ExtractRow(actual, i);

                if (!RowsMatch(expectedRow, actualRow))
                {
                    return false;
                }
            }

            return true;
        }

        public bool[] ExtractRow(bool[,] matrix, int i)
        {
            int n = matrix.GetLength(1);
            bool[] result = new bool[n];

            for (int j = 0; j < n; j++)
            {
                result[j] = matrix[i, j];
            }

            return result;
        }

        public bool RowsMatch(bool[] expected, bool[] actual)
        {
            if (expected.Length != actual.Length)
            {
                return false;
            }

            for (int i = 0; i < expected.Length; i++)
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
