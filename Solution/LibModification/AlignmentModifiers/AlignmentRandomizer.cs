using LibBioInfo;
using LibModification.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.AlignmentModifiers
{
    public class AlignmentRandomizer : AlignmentModifier, IAlignmentModifier
    {
        AlignmentStateHelper StateHelper = new AlignmentStateHelper();
        CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();

        public override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            //if (Randomizer.CoinFlip())
            //{
            //    int extent = Randomizer.Random.Next(alignment.Width);
            //    CharMatrixHelper.SprinkleEmptyColumnsIntoAlignment(alignment, extent);
            //}

            bool[,] bitmask = StateHelper.ConvertMatrixFromCharToBool(in alignment.CharacterMatrix);
            ShuffleMatrixRows(ref bitmask);
            char[,] modified = StateHelper.ConvertMatrixFromBoolToChar(alignment.Sequences, in bitmask);
            return CharMatrixHelper.RemoveEmptyColumns(in modified);
        }

        public void ShuffleMatrixRows(ref bool[,] matrix)
        {
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);
            for (int i = 0; i < m; i++)
            {
                ShuffleRow(ref matrix, i);
            }
        }

        public void ShuffleRow(ref bool[,] matrix, int i)
        {
            int n = matrix.GetLength(1);

            List<Tuple<int, bool>> pairs = new List<Tuple<int, bool>>();
            for (int j = 0; j < n; j++)
            {
                int randomValue = Randomizer.Random.Next(0, int.MaxValue);
                pairs.Add(new Tuple<int, bool>(randomValue, matrix[i, j]));
            }

            pairs = pairs.OrderBy(x => x.Item1).ToList();

            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = pairs[j].Item2;
            }
        }
    }
}
