using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.IAlignmentModifiers
{
    public enum SwapDirection
    {
        Left,
        Right
    }
    
    public class SwapOperator : IAlignmentModifier
    {
        public void ModifyAlignment(Alignment alignment)
        {
            throw new NotImplementedException();
        }

        public void Swap(Alignment alignment, int i, int j, int k, SwapDirection direction)
        {
            // affects ith sequence only
            // from column j, 

            if (direction == SwapDirection.Left)
            {
                SwapLeft(alignment, i, j, k);
            }
            else
            {
                SwapRight(alignment, i, j, k);
            }
        }

        public void SwapLeft(Alignment alignment, int i, int j, int k)
        {

        }

        public void SwapRight(Alignment alignment, int i, int j, int k)
        {

        }
    }
}
