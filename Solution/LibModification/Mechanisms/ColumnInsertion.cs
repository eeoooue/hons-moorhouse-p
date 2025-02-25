using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.Mechanisms
{
    public static class ColumnInsertion
    {
        public static void InsertEmptyColumn(Alignment alignment, int j)
        {
            char[,] matrix = InsertEmptyColumn(alignment.CharacterMatrix, j);
            alignment.CharacterMatrix = matrix;
        }

        public static char[,] InsertEmptyColumn(char[,] matrix, int j)
        {
            int m = matrix.GetLength(0);
            bool[] mask = new bool[m];
            return GapInsertion.InsertGaps(matrix, mask, 1, j, j);
        }
    }
}
