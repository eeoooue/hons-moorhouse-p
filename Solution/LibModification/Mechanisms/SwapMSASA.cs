using LibBioInfo;
using LibModification.AlignmentModifiers;
using LibModification.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.Mechanisms
{
    public static class SwapMSASA
    {
        public static CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();

        public static void Swap(Alignment alignment, int i, int j, int k, SwapDirection direction)
        {
            Swap(ref alignment.CharacterMatrix, i, j, k, direction);
        }

        
        public static void Swap(ref char[,] matrix, int i, int j, int k, SwapDirection direction)
        {
            // affects ith sequence only
            // from column j, 

            string payload = CharMatrixHelper.GetCharRowAsString(in matrix, i);
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

        public static string SwapRight(string payload, int i, int j, int k)
        {
            List<int> residuesToMove = CollectResiduesToMove(payload, j, k);
            return PerformSwap(payload, j, residuesToMove);
        }

        public static List<int> CollectResiduesToMove(string sequence, int startPos, int k)
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

        public static string PerformSwap(string payload, int startPos, List<int> residuePositions)
        {
            if (residuePositions.Count == 0)
            {
                return payload;
            }

            char[] result = payload.ToCharArray();
            foreach (int i in residuePositions)
            {
                result[i] = '-';
            }

            int residuesPlaced = 0;
            for (int i = startPos; i < result.Length; i++)
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

        public static string SwapLeft(string payload, int i, int j, int k)
        {
            string mirrored = GetReversedString(payload);
            string modifiedInReverse = SwapRight(mirrored, i, j, k);
            string modified = GetReversedString(modifiedInReverse);
            return modified;
        }

        public static string GetReversedString(string original)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = original.Length - 1; i >= 0; i--)
            {
                sb.Append(original[i]);
            }

            return sb.ToString();
        }

        #endregion
    }
}
