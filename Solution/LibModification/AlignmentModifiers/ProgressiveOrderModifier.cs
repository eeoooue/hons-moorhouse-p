using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.AlignmentModifiers
{
    public class ProgressiveOrderModifier : IAlignmentModifier
    {
        public void ModifyAlignment(Alignment alignment)
        {
            SwapRandomIndicesWithinArray(ref alignment.ProgressiveOrder);
        }

        public void SwapRandomIndicesWithinArray(ref int[] arr)
        {
            int i = Randomizer.Random.Next(arr.Length);
            int j = i;

            while (j == i)
            {
                j = Randomizer.Random.Next(arr.Length);
            }

            SwapIndicesWithinArray(ref arr, i, j);
        }

        public void SwapIndicesWithinArray(ref int[] arr, int i, int j)
        {
            int x = arr[i];
            int y = arr[j];
            arr[i] = y;
            arr[j] = x;
        }
    }
}
