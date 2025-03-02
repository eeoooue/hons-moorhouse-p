using LibBioInfo;
using LibModification.Helpers;
using LibModification.Mechanisms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.AlignmentModifiers
{
    public enum SwapDirection
    {
        Left,
        Right
    }

    public class SwapOperator : AlignmentModifier, IAlignmentModifier
    {
        public BiosequencePayloadHelper PayloadHelper = new BiosequencePayloadHelper();
        public CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();

        public override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            int i = Randomizer.Random.Next(alignment.Height);
            char[,] matrix = alignment.CharacterMatrix;
            PerformSwapWithinRow(ref matrix, i);
            return CharMatrixHelper.RemoveEmptyColumns(in matrix);
        }

        public void PerformSwapWithinRow(ref char[,] matrix, int i)
        {
            int n = matrix.GetLength(1);
            int j = n;
            int k = n;
            while (j + k >= n)
            {
                j = Randomizer.Random.Next(n);
                k = Randomizer.Random.Next(1, n / 2);
            }

            SwapDirection direction = Randomizer.CoinFlip() ? SwapDirection.Left : SwapDirection.Right;

            SwapMSASA.Swap(ref matrix, i, j, k, direction);
        }
    }
}
