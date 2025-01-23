using LibBioInfo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.LegacyAlignmentModifiers
{
    public class MultiRowStochasticSwapOperator : AlignmentModifier, ILegacyAlignmentModifier
    {
        private SwapOperator SwapOperator = new SwapOperator();
        private CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();

        protected override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            char[,] matrix = alignment.CharacterMatrix;

            for (int i = 0; i < alignment.Height; i++)
            {
                if (Randomizer.CoinFlip())
                {
                    SwapOperator.PerformSwapWithinRow(ref matrix, i);
                }
            }

            return CharMatrixHelper.RemoveEmptyColumns(in matrix);
        }
    }
}
