using LibBioInfo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.LegacyAlignmentModifiers
{
    public enum SwapDirection
    {
        Left,
        Right
    }

    public class SwapOperator : AlignmentModifier, ILegacyAlignmentModifier
    {
        public BiosequencePayloadHelper PayloadHelper = new BiosequencePayloadHelper();
        public CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();
        public Bioinformatics Bioinformatics = new Bioinformatics();
        protected override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            int i = Randomizer.Random.Next(alignment.Height);
            char[,] matrix = alignment.CharacterMatrix;
            PerformSwapWithinRow(ref matrix, i);
            return CharMatrixHelper.RemoveEmptyColumns(ref matrix);
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

            if (Randomizer.CoinFlip())
            {
                Swap(ref matrix, i, j, k, SwapDirection.Left);
            }
            else
            {
                Swap(ref matrix, i, j, k, SwapDirection.Right);
            }
        }

        public void Swap(ref char[,] matrix, int i, int j, int k, SwapDirection direction)
        {
            // affects ith sequence only
            // from column j, 

            string payload = CharMatrixHelper.GetCharRowAsString(ref matrix, i);
            string modified;

            if (direction == SwapDirection.Left)
            {
                modified = SwapLeft(payload, i, j, k);
            }
            else
            {
                modified = SwapRight(payload, i, j, k);
            }

            CharMatrixHelper.WriteStringOverMatrixRow(ref matrix, i, modified);
        }

        public string SwapRight(string payload, int i, int j, int k)
        {
            List<int> residuesToMove = CollectResiduesToMove(payload, j, k);
            return PerformSwap(payload, j, residuesToMove);
        }

        public List<int> CollectResiduesToMove(string sequence, int startPos, int k)
        {
            List<int> result = new List<int>();

            for (int j = startPos; j < sequence.Length; j++)
            {
                bool isResidue = !Bioinformatics.IsGapChar(sequence[j]);

                if (isResidue)
                {
                    result.Add(j);

                    if (result.Count == k)
                    {
                        return result;
                    }
                }
            }

            return result;
        }

        public string PerformSwap(string payload, int startPos, List<int> residuePositions)
        {
            if (residuePositions.Count == 0)
            {
                return payload;
            }

            char[] result = payload.ToCharArray();
            foreach(int i in residuePositions)
            {
                result[i] = '-';
            }

            int residuesPlaced = 0;
            for(int i=startPos; i<result.Length; i++)
            {
                if (Bioinformatics.IsGapChar(result[i]))
                {
                    int p = residuePositions[residuesPlaced++];
                    result[i] = payload[p];
                }

                if (residuesPlaced == residuePositions.Count)
                {
                    return new string(result);
                }
            }

            return new string(result);
        }

        #region Leftwards swap via mirroring

        public string SwapLeft(string payload, int i, int j, int k)
        {
            string mirrored = GetReversedString(payload);
            string modifiedInReverse = SwapRight(mirrored, i, j, k);
            string modified = GetReversedString(modifiedInReverse);
            return modified;
        }

        public string GetReversedString(string original)
        {
            StringBuilder sb = new StringBuilder();
            for(int i=original.Length-1; i>=0; i--)
            {
                sb.Append(original[i]);
            }

            return sb.ToString();
        }

        #endregion
    }
}
