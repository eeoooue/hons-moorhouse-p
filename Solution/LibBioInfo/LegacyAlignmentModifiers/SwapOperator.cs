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

    public class SwapOperator : ILegacyAlignmentModifier, IAlignmentModifier
    {
        public BiosequencePayloadHelper PayloadHelper = new BiosequencePayloadHelper();
        public CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();
        public Bioinformatics Bioinformatics = new Bioinformatics();

        public void ModifyAlignment(Alignment alignment)
        {
            char[,] modified = GetModifiedAlignmentState(alignment);
            alignment.CharacterMatrix = modified;
        }

        public char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            int i = Randomizer.Random.Next(alignment.Height);
            char[,] modified = PerformSwapWithinRow(alignment.CharacterMatrix, i);
            return CharMatrixHelper.RemoveEmptyColumns(modified);
        }


        public char[,] PerformSwapWithinRow(char[,] matrix, int i)
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
                return Swap(matrix, i, j, k, SwapDirection.Left);
            }
            else
            {
                return Swap(matrix, i, j, k, SwapDirection.Right);
            }
        }

        public char[,] Swap(char[,] matrix, int i, int j, int k, SwapDirection direction)
        {
            // affects ith sequence only
            // from column j, 

            if (direction == SwapDirection.Left)
            {
                return SwapLeft(matrix, i, j, k);
            }
            else
            {
                return SwapRight(matrix, i, j, k);
            }
        }

        public char[,] SwapRight(char[,] matrix, int i, int j, int k)
        {
            string payload = CharMatrixHelper.GetCharRowAsString(matrix, i);

            List<int> residuesToMove = CollectResiduesToMove(payload, j, k);
            string modified = PerformSwap(payload, j, residuesToMove);

            char[,] result = CharMatrixHelper.WriteStringOverMatrixRow(matrix, i, modified);
            return result;
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
        public char[,] SwapLeft(char[,] matrix, int i, int j, int k)
        {
            char[,] mirrored = GetHorizontallyMirroredState(matrix);
            char[,] modified = SwapRight(mirrored, i, j, k);
            char[,] newState = GetHorizontallyMirroredState(modified);
            return newState;
        }

        public char[,] GetHorizontallyMirroredState(char[,] state)
        {
            int m = state.GetLength(0);
            int n = state.GetLength(1);

            char[,] result = new char[m, n];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int j2 = -1 + n - j;
                    result[i, j] = state[i, j2];
                }
            }

            return result;
        }

        #endregion
    }
}
