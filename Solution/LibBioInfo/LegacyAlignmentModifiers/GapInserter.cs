using LibBioInfo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.LegacyAlignmentModifiers
{
    public class GapInserter : AlignmentModifier, ILegacyAlignmentModifier
    {
        public BiosequencePayloadHelper PayloadHelper = new BiosequencePayloadHelper();
        public CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();

        public int GapWidthLimit;

        public GapInserter(int gapSizeLimit = 4)
        {
            GapWidthLimit = gapSizeLimit;
        }

        protected override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            int gapWidth = PickGapWidth();
            char[,] modified = InsertGapOfWidth(in alignment.CharacterMatrix, gapWidth);
            return CharMatrixHelper.RemoveEmptyColumns(in modified);
        }

        public char[,] InsertGapOfWidth(in char[,] matrix, int gapWidth)
        {
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1) + gapWidth;
            int position1 = SuggestGapPosition(gapWidth, n);
            int position2 = SuggestGapPosition(gapWidth, n);

            bool[] mapping = GetRowMapping(m);

            char[,] result = new char[m, n];

            for (int i = 0; i < m; i++)
            {
                string payload = CharMatrixHelper.GetCharRowAsString(in matrix, i);

                if (mapping[i])
                {
                    payload = PayloadHelper.GetPayloadWithGapInserted(payload, gapWidth, position1);
                }
                else
                {
                    payload = PayloadHelper.GetPayloadWithGapInserted(payload, gapWidth, position2);
                }

                CharMatrixHelper.WriteStringOverMatrixRow(ref result, i, payload);
            }

            return result;
        }

        public int SuggestGapPosition(int gapWidth, int stateWidth)
        {
            int n = stateWidth - gapWidth;
            return Randomizer.Random.Next(0, n);
        }

        public bool[] GetRowMapping(int n)
        {
            bool[] result = new bool[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = Randomizer.CoinFlip();
            }

            return result;
        }

        public int PickGapWidth()
        {
            int gapWidth = Randomizer.Random.Next(1, GapWidthLimit + 1);
            return gapWidth;
        }

        
    }
}
