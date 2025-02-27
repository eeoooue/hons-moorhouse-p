using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.BlockShuffling
{
    internal class BlockFinder
    {
        // aims to find a block of ones based on logic described in SAGA

        public void FindBlock(MaskedAlignment alignment, ref bool[] sequences, ref List<int> startPositions, ref List<int> endPositions)
        {
            PickStartingPosition(alignment, sequences, out int iStart, out int jStart);

            for(int i=0; i<alignment.Height; i++)
            {
                if (!sequences[i])
                {
                    continue;
                }

                bool hasStarter = alignment.Mask[i, jStart];
                if (!hasStarter)
                {
                    sequences[i] = false;
                    continue;
                }

                ExpandBlockWidth(alignment, i, jStart, out int jFrontInclusive, out int jEndInclusive);
                startPositions.Add(jFrontInclusive);
                endPositions.Add(jEndInclusive);
            }
        }

        public bool HasBlockStarter(MaskedAlignment alignment, int i, int j)
        {
            return alignment.Mask[i, j];
        }

        public void ExpandBlockWidth(MaskedAlignment alignment, int i, int jStart, out int jFrontInclusive, out int jEndInclusive)
        {
            jFrontInclusive = jStart;
            while (MaskContainsBitAt(alignment, i, jFrontInclusive - 1))
            {
                jFrontInclusive -= 1;
            }

            jEndInclusive = jStart;
            while (MaskContainsBitAt(alignment, i, jEndInclusive + 1))
            {
                jFrontInclusive += 1;
            }
        }

        public bool MaskContainsBitAt(MaskedAlignment alignment, int i, int j)
        {
            if (0 <= i && i < alignment.Height)
            {
                if (0 <= j && j < alignment.Width)
                {
                    return alignment.Mask[i, j];
                }
            }

            return false;
        }

        public void PickStartingPosition(MaskedAlignment alignment, bool[] sequences, out int i, out int j)
        {
            List<int> sequenceOptions = GetMaskAsListOfIndices(sequences);
            i = PickIntegerFromList(sequenceOptions);

            List<int> optionsWithinRow = GetOnesInRowOfMask(alignment, i);
            j = PickIntegerFromList(optionsWithinRow);
        }

        public List<int> GetMaskAsListOfIndices(bool[] sequences)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < sequences.Length; i++)
            {
                if (sequences[i])
                {
                    result.Add(i);
                }
            }
            return result;
        }

        public List<int> GetOnesInRowOfMask(MaskedAlignment alignment, int i)
        {
            List<int> result = new List<int>();
            for (int j=0; j<alignment.Width; j++)
            {
                if (alignment.Mask[i, j])
                {
                    result.Add(j);
                }
            }

            return result;
        }

        public int PickIntegerFromList(List<int> options)
        {
            int n = options.Count;
            int i = Randomizer.Random.Next(n);

            return options[i];
        }
    }
}
