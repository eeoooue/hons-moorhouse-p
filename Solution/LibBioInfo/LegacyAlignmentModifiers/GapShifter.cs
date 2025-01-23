using LibBioInfo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.LegacyAlignmentModifiers
{
    public class GapShifter : ILegacyAlignmentModifier
    {
        public CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();
        public BiosequencePayloadHelper PayloadHelper = new BiosequencePayloadHelper();

        public void ModifyAlignment(Alignment alignment)
        {
            char[,] state = GetModifiedAlignmentState(alignment);
            alignment.CharacterMatrix = state;
        }

        public char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            char[,] matrix = alignment.CharacterMatrix;

            while (true)
            {
                int i = Randomizer.Random.Next(alignment.Height);
                bool possible = CharMatrixHelper.RowContainsGap(alignment.CharacterMatrix, i);

                if (possible)
                {
                    return GetMatrixWithGapShiftInRow(matrix, i);
                }
            }
        }

        public char[,] GetMatrixWithGapShiftInRow(char[,] matrix, int i)
        {
            List<int> residuePositions = CharMatrixHelper.GetResiduePositionsInRow(matrix, i);
            List<int> gapPositions = CharMatrixHelper.GetGapPositionsInRow(matrix, i);

            string payload = CharMatrixHelper.GetCharRowAsString(matrix, i);

            int chosenGapPosition = GetRandomChoiceFromList(gapPositions);
            string payloadWithGapRemoved = PayloadHelper.GetPayloadWithGapRemovedAt(payload, chosenGapPosition);

            int newGapPos = GetRandomChoiceFromList(residuePositions);
            string payloadWithGapInserted = PayloadHelper.GetPayloadWithGapInsertedAt(payloadWithGapRemoved, newGapPos);

            char[,] result = CharMatrixHelper.WriteStringOverMatrixRow(matrix, i, payloadWithGapInserted);

            return result;
        }

        private int GetRandomChoiceFromList(List<int> options)
        {
            int i = Randomizer.Random.Next(options.Count);
            return options[i];
        }
    }
}
