using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.INeighbourhoodFinders
{
    public class SwapBasedNeighbourhoodFinder : INeighbourhoodFinder
    {
        private RowBasedNeighbourhoodFinder RowFinder = new RowBasedNeighbourhoodFinder();

        public List<bool[,]> FindNeighbours(bool[,] state)
        {
            int m = state.GetLength(0);

            List<bool[,]> result = new List<bool[,]>();

            for(int i=0; i<m; i++)
            {
                List<bool[,]> neighbours = RowFinder.FindNeighboursForRow(state, i);
                foreach (bool[,] neighbour in neighbours)
                {
                    result.Add(neighbour);
                }
            }

            return result;
        }
    }
}
