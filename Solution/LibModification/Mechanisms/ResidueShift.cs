using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.Mechanisms
{
    public enum ShiftDirection
    {
        Leftwise,
        Rightwise,
    }

    public static class ResidueShift
    {
        public static void ShiftResidue(Alignment alignment, int i, int j, ShiftDirection direction)
        {
            char[,] matrix = ShiftResidue(alignment.CharacterMatrix, i, j, direction);
            alignment.CharacterMatrix = matrix;
        }

        public static char[,] ShiftResidue(char[,] original, int i, int j, ShiftDirection direction)
        {
            if (Bioinformatics.IsGap(original[i, j]))
            {
                return original;
            }

            switch (direction)
            {
                case ShiftDirection.Leftwise:
                    return ShiftResidueLeft(original, i, j);
                case ShiftDirection.Rightwise:
                default:
                    return ShiftResidueRight(original, i, j);
            }
        }

        public static char[,] ShiftResidueLeft(char[,] original, int i, int j)
        {
            int gapIndex = GetEarliestIndexOfGap(original, i, j, -1);

            if (gapIndex == -1)
            {
                // TODO: FIX LOGIC

                //char[,] matrix = ColumnInsertion.InsertEmptyColumn(original, 0);
                //Console.WriteLine($"matrix has with = {matrix.GetLength(1)}");
                //return ShiftResidueLeft(matrix, i, j);

                return original;
            }

            int j2 = gapIndex;
            while (j2 < j)
            {
                original[i, j2] = original[i, j2 + 1];
                j2++;
            }
            original[i, j2] = '-';

            return original;
        }

        public static char[,] ShiftResidueRight(char[,] original, int i, int j)
        {
            int gapIndex = GetEarliestIndexOfGap(original, i, j, 1);

            if (gapIndex == -1)
            {
                // TODO: FIX LOGIC

                //int n = original.GetLength(1);
                //char[,] matrix = ColumnInsertion.InsertEmptyColumn(original, n);
                //return ShiftResidueRight(matrix, i, j);

                return original;
            }

            int j2 = gapIndex;
            while (j < j2)
            {
                original[i, j2] = original[i, j2 - 1];
                j2--;
            }
            original[i, j2] = '-';

            return original;
        }

        public static int GetEarliestIndexOfGap(in char[,] matrix, int i, int j, int delta)
        {
            int n = matrix.GetLength(1);
            while (true)
            {
                j += delta;
                if (j < 0 || n <= j)
                {
                    return -1;
                }

                char x = matrix[i, j];
                if (Bioinformatics.IsGap(x))
                {
                    return j;
                }
            }
        }
    }
}
