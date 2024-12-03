using LibBioInfo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.INeighbourhoodFinders
{
    public class RowBasedNeighbourhoodFinder : INeighbourhoodFinder
    {
        AlignmentStateHelper StateHelper = new AlignmentStateHelper();

        public List<bool[,]> FindNeighbours(bool[,] state)
        {
            int i = Randomizer.Random.Next(state.GetLength(0));
            return FindNeighboursForRow(state, i);
        }

        public List<bool[,]> FindNeighboursForRow(bool[,] state, int i)
        {
            int m = state.GetLength(0);
            bool[] originalRow = StateHelper.ExtractRow(state, i);

            List<bool[,]> result = new List<bool[,]>();

            foreach (bool[] row in GetNeighboursOfRow(originalRow))
            {
                bool[,] neighbour = GetStateWithReplacedRow(state, i, row);
                result.Add(neighbour);
            }

            return result;
        }

        public bool[,] GetStateWithReplacedRow(bool[,] state, int rowIndex, bool[] row)
        {
            int m = state.GetLength(0);
            int n = state.GetLength(1);

            bool[,] result = new bool[m, n];

            for(int i=0; i<m; i++)
            {
                if (i == rowIndex)
                {
                    continue;
                }
                for(int j=0; j<n; j++)
                {
                    result[i, j] = state[i, j];
                }
            }

            for(int j=0; j<n; j++)
            {
                result[rowIndex, j] = row[j];
            }

            return result;
        }

        public List<bool[]> GetNeighboursOfRow(bool[] row)
        {
            List<bool[]> result = new List<bool[]>();
            for(int i=0; i<row.Length; i++)
            {
                for(int j=i+1; j<row.Length; j++)
                {
                    if (row[i] != row[j])
                    {
                        bool[] neighbour = GetRowAfterSwap(row, i, j);
                        result.Add(neighbour);
                    }
                }
            }

            return result;
        }

        public bool[] GetRowAfterSwap(bool[] original, int a, int b)
        {
            bool[] result = new bool[original.Length];
            for(int i=0; i< original.Length; i++)
            {
                result[i] = original[i];
            }
            result[a] = original[b];
            result[b] = original[a];

            return result;
        }
    }
}
