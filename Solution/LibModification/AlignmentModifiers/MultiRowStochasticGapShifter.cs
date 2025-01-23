using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.AlignmentModifiers
{
    public class MultiRowStochasticGapShifter : AlignmentModifier, IAlignmentModifier
    {
        private GapShifter GapShifter = new GapShifter();

        protected override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            char[,] matrix = alignment.CharacterMatrix;

            for (int i = 0; i < alignment.Height; i++)
            {
                if (Randomizer.CoinFlip())
                {
                    GapShifter.PerformGapShiftInRow(ref matrix, i);
                }
            }

            return matrix;
        }
    }
}
