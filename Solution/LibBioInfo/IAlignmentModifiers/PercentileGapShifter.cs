using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.IAlignmentModifiers
{
    public class PercentileGapShifter : ILegacyAlignmentModifier
    {
        private GapShifter GapShifter = new GapShifter();
        public double Percentage;

        public PercentileGapShifter(double percentage=0.05)
        {
            Percentage = percentage;
        }

        public void ModifyAlignment(Alignment alignment)
        {
            for(int i=0; i<alignment.Height; i++)
            {
                int shifts = DecideNumberOfShiftsToAttempt(alignment, i);
                for(int j=0; j<shifts; j++)
                {
                    GapShifter.TryShiftGapInRow(alignment, i);
                }
            }
            alignment.CheckResolveEmptyColumns();
        }

        public int DecideNumberOfShiftsToAttempt(Alignment alignment, int i)
        {
            BioSequence sequence = alignment.Sequences[i];
            int residues = sequence.Residues.Length;
            int gaps = alignment.Width - residues;
            int shifts = (int)Math.Ceiling(Percentage * gaps);
            return shifts;
        }
    }
}
