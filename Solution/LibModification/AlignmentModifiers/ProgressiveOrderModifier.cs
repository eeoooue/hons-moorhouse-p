using LibBioInfo;
using LibBioInfo.PairwiseAligners;
using LibModification.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.AlignmentModifiers
{
    public class ProgressiveOrderModifier : AlignmentModifier
    {
        CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();
        BiosequencePayloadHelper PayloadHelper = new BiosequencePayloadHelper();
        Bioinformatics Bioinformatics = new Bioinformatics();
        HeuristicPairwiseModifier PairwiseModifier = new HeuristicPairwiseModifier();

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

        public override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            int i;
            int j;
            PickRandomPairOfSequences(alignment, out i, out j);
            SwapIndicesWithinArray(ref alignment.ProgressiveOrder, i, j);

            if (Randomizer.CoinFlip())
            {
                return AlignNeighbourOf(alignment, i);
            }
            else
            {
                return AlignNeighbourOf(alignment, j);
            }
        }

        public char[,] AlignNeighbourOf(Alignment alignment, int i)
        {
            int delta = Randomizer.CoinFlip() ? -1 : 1;
            int j = (i + delta + alignment.Height) % alignment.Height;

            int seqAindex = alignment.ProgressiveOrder[i];
            int seqBindex = alignment.ProgressiveOrder[j];

            return PairwiseModifier.AlignPairOfSequences(alignment, seqAindex, seqBindex);
        }

        public void PickRandomPairOfSequences(Alignment alignment, out int i, out int j)
        {
            i = Randomizer.Random.Next(alignment.Height);
            j = i;
            while (i == j)
            {
                j = Randomizer.Random.Next(alignment.Height);
            }
        }
    }
}
