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

    public class ResidueShift
    {
        public void ShiftResidue(Alignment alignment, int i, int j, ShiftDirection direction)
        {
            char[,] matrix = ShiftResidue(alignment.CharacterMatrix, i, j, direction);
            alignment.CharacterMatrix = matrix;
        }

        public char[,] ShiftResidue(char[,] original, int i, int j, ShiftDirection direction)
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

        public char[,] ShiftResidueLeft(char[,] original, int i, int j)
        {
            int gapIndex = GetEarliestIndexOfGap(original, i, j, -1);

            if (gapIndex == -1)
            {
                throw new NotImplementedException();
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

        public char[,] ShiftResidueRight(char[,] original, int i, int j)
        {
            int gapIndex = GetEarliestIndexOfGap(original, i, j, 1);

            if (gapIndex == -1)
            {
                throw new NotImplementedException();
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

        public int GetEarliestIndexOfGap(in char[,] matrix, int i, int j, int delta)
        {
            int n = matrix.GetLength(1);
            while (0 <= j && j < n)
            {
                j += delta;
                char x = matrix[i, j];
                if (Bioinformatics.IsGap(x))
                {
                    return j;
                }
            }

            return -1;
        }
    }
}
