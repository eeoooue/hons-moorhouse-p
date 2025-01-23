﻿using LibBioInfo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.LegacyAlignmentModifiers
{
    public class AlignmentRandomizer : ILegacyAlignmentModifier, IAlignmentModifier
    {
        AlignmentStateHelper StateHelper = new AlignmentStateHelper();
        CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();

        public void ModifyAlignment(Alignment alignment)
        {
            char[,] modifiedMat = GetModifiedAlignmentState(alignment);
            alignment.CharacterMatrix = modifiedMat;
        }

        public char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            bool[,] originalState = StateHelper.ConvertMatrixFromCharToBool(alignment.CharacterMatrix);
            bool[,] state = GetMatrixWithShuffledRows(originalState);
            char[,] modifiedMat = StateHelper.ConvertMatrixFromBoolToChar(alignment.Sequences, state);
            return CharMatrixHelper.RemoveEmptyColumns(ref modifiedMat);
        }

        public bool[,] GetMatrixWithShuffledRows(bool[,] matrix)
        {
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);

            bool[,] result = new bool[m, n];

            for (int i = 0; i < m; i++)
            {
                bool[] shuffledRow = GetShuffledRow(matrix, i);
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = shuffledRow[j];
                }
            }

            return result;
        }

        public bool[] GetShuffledRow(bool[,] matrix, int i)
        {
            int n = matrix.GetLength(1);
            bool[] result = new bool[n];

            List<Tuple<int, bool>> pairs = new List<Tuple<int, bool>>();

            for (int j = 0; j < n; j++)
            {
                int randomValue = Randomizer.Random.Next(0, int.MaxValue);
                pairs.Add(new Tuple<int, bool>(randomValue, matrix[i, j]));
            }

            pairs = pairs.OrderBy(x => x.Item1).ToList();

            for (int j = 0; j < n; j++)
            {
                result[j] = pairs[j].Item2;
            }

            return result;
        }
    }
}
