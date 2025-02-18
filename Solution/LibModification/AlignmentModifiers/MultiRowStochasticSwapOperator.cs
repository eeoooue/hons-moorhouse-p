using LibBioInfo;
using LibModification.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.AlignmentModifiers
{
    public class MultiRowStochasticSwapOperator : AlignmentModifier, IAlignmentModifier
    {
        private SwapOperator SwapOperator = new SwapOperator();
        private CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();

        public override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            if (Randomizer.CoinFlip())
            {
                int extent = Randomizer.Random.Next(alignment.Width);
                CharMatrixHelper.SprinkleEmptyColumnsIntoAlignment(alignment, extent);
            }

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
