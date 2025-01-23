using LibBioInfo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.AlignmentModifiers
{
    public class GapShifter : AlignmentModifier, IAlignmentModifier
    {
        public CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();
        public BiosequencePayloadHelper PayloadHelper = new BiosequencePayloadHelper();

        protected override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            char[,] matrix = alignment.CharacterMatrix;

            while (true)
            {
                int i = Randomizer.Random.Next(alignment.Height);
                bool possible = CharMatrixHelper.RowContainsGap(alignment.CharacterMatrix, i);

                if (possible)
                {
                    PerformGapShiftInRow(ref matrix, i);
                    return CharMatrixHelper.RemoveEmptyColumns(in matrix);
                }
            }
        }

        public void PerformGapShiftInRow(ref char[,] matrix, int i)
        {
            List<int> residuePositions = CharMatrixHelper.GetResiduePositionsInRow(matrix, i);
            List<int> gapPositions = CharMatrixHelper.GetGapPositionsInRow(matrix, i);

            string payload = CharMatrixHelper.GetCharRowAsString(in matrix, i);

            int chosenGapPosition = GetRandomChoiceFromList(gapPositions);
            string payloadWithGapRemoved = PayloadHelper.GetPayloadWithGapRemovedAt(payload, chosenGapPosition);

            int newGapPos = GetRandomChoiceFromList(residuePositions);
            string payloadWithGapInserted = PayloadHelper.GetPayloadWithGapInsertedAt(payloadWithGapRemoved, newGapPos);

            CharMatrixHelper.WriteStringOverMatrixRow(ref matrix, i, payloadWithGapInserted);
        }

        private int GetRandomChoiceFromList(List<int> options)
        {
            int i = Randomizer.Random.Next(options.Count);
            return options[i];
        }
    }
}
