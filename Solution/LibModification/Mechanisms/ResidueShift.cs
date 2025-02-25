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

    internal class ResidueShift
    {
        public void ShiftResidue(Alignment alignment, int i, int j, ShiftDirection direction)
        {
            char[,] matrix = ShiftResidue(alignment.CharacterMatrix, i, j, direction);
            alignment.CharacterMatrix = matrix;
        }

        public char[,] ShiftResidue(char[,] original, int i, int j, ShiftDirection direction)
        {
            switch (direction)
            {
                case ShiftDirection.Leftwise:
                    return ShiftResidueLeft(original, i, j);
                default:
                    return ShiftResidueRight(original, i, j);
            }
        }

        public char[,] ShiftResidueLeft(char[,] original, int i, int j)
        {
            throw new NotImplementedException();
        }

        public char[,] ShiftResidueRight(char[,] original, int i, int j)
        {
            throw new NotImplementedException();
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
