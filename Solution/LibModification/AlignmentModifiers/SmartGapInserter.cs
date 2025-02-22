using LibBioInfo;
using LibModification.Helpers;
using LibSimilarity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.AlignmentModifiers
{
    public class SmartGapInserter : AlignmentModifier, IAlignmentModifier
    {
        public BiosequencePayloadHelper PayloadHelper = new BiosequencePayloadHelper();
        public CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();

        public int GapWidthLimit;

        public SmartGapInserter(int gapSizeLimit = 4)
        {
            GapWidthLimit = gapSizeLimit;
        }

        public override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            int gapWidth = PickGapWidth();
            char[,] modified = InsertGapOfWidth(alignment, gapWidth);
            return CharMatrixHelper.RemoveEmptyColumns(in modified);
        }

        public char[,] InsertGapOfWidth(Alignment alignment, int gapWidth)
        {
            int m = alignment.Height;
            int n = alignment.Width + gapWidth;
            int position1 = SuggestGapPosition(gapWidth, n);
            int position2 = SuggestGapPosition(gapWidth, n);

            bool[] mapping = GetRowMapping(alignment);

            char[,] result = new char[m, n];

            for (int i = 0; i < m; i++)
            {
                string payload = CharMatrixHelper.GetCharRowAsString(in alignment.CharacterMatrix, i);

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

        public bool[] GetRowMapping(Alignment alignment)
        {

            HashSet<string> selected = GetIdentifiersOfSetOfSimilarSequences();
            int m = alignment.Height;

            bool[] result = new bool[m];

            for (int i=0; i<m; i++)
            {
                BioSequence sequence = alignment.Sequences[i];
                if (selected.Contains(sequence.Identifier))
                {
                    result[i] = true;
                }
                else
                {
                    result[i] = false;
                }
            }

            return result;
        }


        public HashSet<string> GetIdentifiersOfSetOfSimilarSequences()
        {
            List<BioSequence> selection = SimilarityGuide.GetSetOfSimilarSequences();

            HashSet<string> result = new HashSet<string>();
            foreach (BioSequence sequence in selection)
            {
                result.Add(sequence.Identifier);
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
