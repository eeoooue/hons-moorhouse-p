using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.Helpers
{
    public class AlignmentStateHelper
    {
        public bool[,] RemoveEmptyColumns(bool[,] state)
        {
            List<int> targets = CollectNonEmptyColumnIndices(state);

            int m = state.GetLength(0);
            int n = targets.Count;

            bool[,] result = new bool[m, n];

            for(int j=0; j<n; j++)
            {
                CopyColumnToDestinationFromSource(state, targets[j], result, j);
            }

            return result;
        }

        public void CopyColumnToDestinationFromSource(bool[,] source, int jOrigin, bool[,] destination, int jDest)
        {
            int m = source.GetLength(0);

            for(int i=0; i<m; i++)
            {
                destination[i, jDest] = source[i, jOrigin];
            }
        }

        public List<int> CollectNonEmptyColumnIndices(bool[,] state)
        {
            List<int> result = new List<int>();

            int n = state.GetLength(1);
            for (int j=0; j<n; j++)
            {
                if (!ColumnIsEmpty(state, j))
                {
                    result.Add(j);
                }
            }

            return result;
        }

        public bool ContainsEmptyColumns(bool[,] state)
        {
            int n = state.GetLength(1);

            for (int j = 0; j < n; j++)
            {
                if (ColumnIsEmpty(state, j))
                {
                    return true;
                }
            }
            return false;
        }

        public bool ColumnIsEmpty(bool[,] state, int j)
        {
            int m = state.GetLength(0);

            for (int i = 0; i < m; i++)
            {
                if (!state[i, j])
                {
                    return false;
                }
            }

            return true;
        }

        public bool[,] CombineAlignmentStates(bool[,] leftState, bool[,] rightState)
        {
            int m = leftState.GetLength(0);
            int leftN = leftState.GetLength(1);
            int rightN = rightState.GetLength(1);

            bool[,] result = new bool[m, leftN + rightN];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < leftN; j++)
                {
                    result[i, j] = leftState[i, j];
                }

                for (int j = 0; j < rightN; j++)
                {
                    result[i, j + leftN] = rightState[i, j];
                }
            }

            return result;
        }

        public int GetPositionOfNthResidue(bool[,] state, int i, int n)
        {
            int counter = 0;
            for (int j = 0; j < state.GetLength(1); j++)
            {
                if (state[i, j] == false)
                {
                    counter++;
                }

                if (counter == n)
                {
                    return j;
                }
            }

            return state.GetLength(1);
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

        public int GetNumberOfResiduesInRow(bool[,] state, int i)
        {
            int result = 0;
            for (int j = 0; j < state.GetLength(1); j++)
            {
                if (state[i, j] == false)
                {
                    result++;
                }
            }

            return result;
        }

    }
}
