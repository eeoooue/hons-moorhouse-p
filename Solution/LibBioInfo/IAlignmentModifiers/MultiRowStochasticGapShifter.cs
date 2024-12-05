﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.IAlignmentModifiers
{
    internal class MultiRowStochasticGapShifter : IAlignmentModifier
    {
        private GapShifter GapShifter = new GapShifter();

        public void ModifyAlignment(Alignment alignment)
        {
            for(int i=0; i<alignment.Height; i++)
            {
                if (Randomizer.CoinFlip())
                {
                    GapShifter.TryShiftGapInRow(alignment, i);
                }
            }
        }
    }
}
