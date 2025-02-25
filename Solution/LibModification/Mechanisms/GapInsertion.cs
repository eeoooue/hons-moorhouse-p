using LibBioInfo;
using LibModification.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.Mechanisms
{
    public static class GapInsertion
    {
        public static void InsertGaps(Alignment alignment, bool[] mask, int width, int j1, int j2)
        {
            // j values denote where gaps start. so j can be 0 to n (alignment width)

            char[,] matrix = InsertGaps(alignment.CharacterMatrix, mask, width, j1, j2);
            alignment.CharacterMatrix = matrix;
        }

        public static char[,] InsertGaps(char[,] original, bool[] mask, int width, int j1, int j2)
        {
            int m = original.GetLength(0);
            int n = original.GetLength(1) + width;

            char[,] result = new char[m, n];

            for (int i = 0; i < m; i++)
            {
                int gapPosition = mask[i] ? j1 : j2;
                PasteSequenceWithGapAt(original, result, i, gapPosition, width);
            }

            return result;
        }

        public static void PasteSequenceWithGapAt(in char[,] source, char[,] destination, int i, int gapPosition, int width)
        {
            int n = source.GetLength(1);

            for (int j = 0; j < gapPosition; j++)
            {
                destination[i, j] = source[i, j];
            }

            for (int j = gapPosition; j < gapPosition + width; j++)
            {
                destination[i, j] = '-';
            }

            for (int j=gapPosition; j < n; j++)
            {
                destination[i, j + width] = source[i, j];
            }
        }
    }
}
