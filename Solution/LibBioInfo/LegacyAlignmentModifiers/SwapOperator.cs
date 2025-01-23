﻿using LibBioInfo.Helpers;
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

            string payload = CharMatrixHelper.GetCharRowAsString(matrix, i);
            string modified;

            if (direction == SwapDirection.Left)
            {
                modified = SwapLeft(payload, i, j, k);
            }
            else
            {
                modified = SwapRight(payload, i, j, k);
            }

            char[,] result = CharMatrixHelper.WriteStringOverMatrixRow(matrix, i, modified);

            return result;
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
