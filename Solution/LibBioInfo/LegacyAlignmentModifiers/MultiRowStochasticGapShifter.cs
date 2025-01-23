using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.LegacyAlignmentModifiers
{
    public class MultiRowStochasticGapShifter : ILegacyAlignmentModifier, IAlignmentModifier
    {
        private GapShifter GapShifter = new GapShifter();

        public void ModifyAlignment(Alignment alignment)
        {
            char[,] matrix = GetModifiedAlignmentState(alignment);
            alignment.CharacterMatrix = matrix;
        }

        public char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            char[,] matrix = alignment.CharacterMatrix;

            for (int i = 0; i < alignment.Height; i++)
            {
                if (Randomizer.CoinFlip())
                {
                    matrix = GapShifter.GetMatrixWithGapShiftInRow(matrix, i);
                }
            }

            return matrix;
        }
    }
}
