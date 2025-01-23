using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.IAlignmentModifiers
{
    public class MultiRowStochasticSwapOperator : ILegacyAlignmentModifier
    {
        private SwapOperator SwapOperator = new SwapOperator();

        public void ModifyAlignment(Alignment alignment)
        {
            for (int i = 0; i < alignment.Height; i++)
            {
                if (Randomizer.CoinFlip())
                {
                    SwapOperator.PerformSwapWithinRow(alignment, i);
                }
            }
        }
    }
}
