using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.INeighbourhoodFinders
{
    public class RowBasedNeighbourhoodFinder : INeighbourhoodFinder
    {
        public List<bool[,]> FindNeighbours(bool[,] state)
        {
            int i = Randomizer.Random.Next(state.GetLength(0));
            return FindNeighboursForRow(state, i);
        }

        public List<bool[,]> FindNeighboursForRow(bool[,] state, int i)
        {
            throw new NotImplementedException();
        }

        public bool[] GetStateWithReplacedRow(bool[,] state, int i, bool[] row)
        {
            throw new NotImplementedException();
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
                        result.Append(neighbour);
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
