using LibBioInfo;
using LibModification.Mechanisms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.AlignmentModifiers
{
    public class ResidueShifter : AlignmentModifier
    {
        public override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            ShiftDirection direction = Randomizer.CoinFlip() ? ShiftDirection.Leftwise : ShiftDirection.Rightwise;
            PickResidueIndices(alignment, out int i, out int j);
            ResidueShift.ShiftResidue(alignment, i, j, direction);

            return alignment.CharacterMatrix;
        }

        public void PickResidueIndices(Alignment alignment, out int i, out int j)
        {
            while (true)
            {
                i = Randomizer.Random.Next(alignment.Height);
                j = Randomizer.Random.Next(alignment.Width);
                char x = alignment.CharacterMatrix[i, j];

                if (!Bioinformatics.IsGap(x))
                {
                    return;
                }
            }
        }
    }
}
