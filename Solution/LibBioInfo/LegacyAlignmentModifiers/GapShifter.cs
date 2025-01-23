using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.LegacyAlignmentModifiers
{
    public class GapShifter : ILegacyAlignmentModifier
    {
        public void ModifyAlignment(Alignment alignment)
        {
            char[,] state = GetModifiedAlignmentState(alignment.GetCopy());
            alignment.CharacterMatrix = state;
        }

        public char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            while (true)
            {
                int row = Randomizer.Random.Next(alignment.Height);
                bool success = TryShiftGapInRow(alignment, row);
                if (success)
                {
                    break;
                }
            }

            return alignment.CharacterMatrix;
        }

        public bool TryShiftGapInRow(Alignment alignment, int i)
        {
            throw new NotImplementedException();

            //List<int> residuePositions = alignment.GetResiduePositionsInRow(alignment, i);
            //List<int> gapPositions = alignment.GetGapPositionsInRow(alignment, i);

            //if (residuePositions.Count == 0 || gapPositions.Count == 0)
            //{
            //    return false;
            //}

            //int newGapPos = GetRandomChoiceFromList(residuePositions);
            //int newResiduePos = GetRandomChoiceFromList(gapPositions);
            //alignment.SetState(i, newResiduePos, false);
            //alignment.SetState(i, newGapPos, true);

            //return true;
        }

        private int GetRandomChoiceFromList(List<int> options)
        {
            int i = Randomizer.Random.Next(options.Count);
            return options[i];
        }
    }
}
