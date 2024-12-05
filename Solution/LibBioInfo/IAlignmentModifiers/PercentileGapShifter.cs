﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.IAlignmentModifiers
{
    internal class PercentileGapShifter : IAlignmentModifier
    {
        private GapShifter GapShifter = new GapShifter();

        public double Percentage;

        public PercentileGapShifter(double percentage)
        {
            Percentage = percentage;
        }

        public void ModifyAlignment(Alignment alignment)
        {
            for(int i=0; i<alignment.Height; i++)
            {
                BioSequence sequence = alignment.Sequences[i];
                int residues = sequence.Residues.Length;
                int gaps = alignment.Width - residues;
                int shifts = (int)Math.Ceiling(Percentage * gaps);

                for(int j=0; j<shifts; j++)
                {
                    GapShifter.TryShiftGapInRow(alignment, i);
                }
            }
        }
    }
}
