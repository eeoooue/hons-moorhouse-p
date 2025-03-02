using LibBioInfo;
using LibModification.Helpers;
using LibModification.Mechanisms;
using LibSimilarity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.AlignmentModifiers
{
    public class GapInserter : AlignmentModifier, IAlignmentModifier
    {
        public CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();

        public override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            int gapWidth = PickGapWidth(alignment);
            bool[] mapping = GetRandomRowMapping(alignment.Height);
            SuggestPairOfGapPositions(alignment, out int j1, out int j2);
            GapInsertion.InsertGaps(alignment, mapping, gapWidth, j1, j2);

            return CharMatrixHelper.RemoveEmptyColumns(alignment.CharacterMatrix);
        }

        public void SuggestPairOfGapPositions(Alignment alignment, out int j1, out int j2)
        {
            int limit = alignment.Width + 1;
            j1 = Randomizer.Random.Next(0, limit);
            j2 = j1;

            while (j1 == j2)
            {
                j2 = Randomizer.Random.Next(0, limit);
            }
        }

        public bool[] GetRandomRowMapping(int n)
        {
            bool[] result = new bool[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = Randomizer.CoinFlip();
            }

            return result;
        }

        public int PickGapWidth(Alignment alignment)
        {
            int n = alignment.Width;
            int gapWidth = Randomizer.Random.Next(1, n + 1);
            return gapWidth;
        }
    }
}
