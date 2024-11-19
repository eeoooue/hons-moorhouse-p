using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.IAlignmentModifiers
{
    public class GapShifter : IAlignmentModifier
    {
        private int MaxAttempts = 10;

        public void ModifyAlignment(Alignment alignment)
        {
            while (true)
            {
                int row = Randomizer.Random.Next(alignment.Height);
                bool success = TryShiftGapInRow(alignment, row);
                if (success)
                {
                    return;
                }
            }
        }

        public bool TryShiftGapInRow(Alignment alignment, int i)
        {
            List<int> residuePositions = alignment.GetResiduePositionsInRow(alignment, i);
            List<int> gapPositions = alignment.GetGapPositionsInRow(alignment, i);

            if (residuePositions.Count == 0 || gapPositions.Count == 0)
            {
                return false;
            }

            int newGapPos = GetRandomChoiceFromList(residuePositions);
            int newResiduePos = GetRandomChoiceFromList(gapPositions);

            alignment.State[i, newResiduePos] = false;
            alignment.State[i, newGapPos] = true;

            return true;
        }

        private int GetRandomChoiceFromList(List<int> options)
        {
            int i = Randomizer.Random.Next(options.Count);
            return options[i];
        }
    }
}
