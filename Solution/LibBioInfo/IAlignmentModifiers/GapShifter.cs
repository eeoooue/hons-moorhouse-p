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
            for(int repeat=0; repeat<MaxAttempts; repeat++)
            {
                int column = Randomizer.Random.Next(alignment.Height);
                bool success = TryShiftGapInRow(alignment, column);
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
                int residueChoiceIndex = Randomizer.Random.Next(residuePositions.Count);
                int gapChoiceIndex = Randomizer.Random.Next(gapPositions.Count);

                int posToBecomeGap = residuePositions[residueChoiceIndex];
                int posToBecomeResidue = gapPositions[gapChoiceIndex];

                alignment.State[i, posToBecomeResidue] = false;
                alignment.State[i, posToBecomeGap] = true;

                return true;
            }

            return false;
        }
    }
}
