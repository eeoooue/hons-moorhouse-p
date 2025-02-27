using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.BlockShuffling
{
    public struct PaddingInstructions
    {
        public int Start = 0;
        public int End = 0;
        public int WidthGoal = 0;

        public PaddingInstructions(int start, int end, int widthGoal)
        {
            Start = start;
            End = end;
            WidthGoal = widthGoal;
        }
    }

    public class BlockFinder
    {
        // aims to find a block of ones based on logic described in SAGA

        public CharacterBlock FindBlock(MaskedAlignment alignment, ref bool[] sequences)
        {
            List<int> startPositions = new List<int>();
            List<int> endPositions = new List<int>();
            Console.WriteLine("FindBlock() called");
            FindBlock(alignment, ref sequences, ref startPositions, ref endPositions);

            List<int> sequenceIndices = GetMaskAsListOfIndices(sequences);

            return CreateBlock(alignment, sequenceIndices, startPositions, endPositions);
        }

        public CharacterBlock CreateBlock(in MaskedAlignment alignment, in List<int> sequenceIndices, in List<int> startPositions, in List<int> endPositions)
        {
            int n = startPositions.Count;
            if (n != endPositions.Count)
            {
                throw new Exception($"Number of start positions (x{startPositions.Count}) & end positions (x{endPositions.Count}) were unequal.");
            }
            if (n != sequenceIndices.Count)
            {
                throw new Exception($"Number of start positions (x{startPositions.Count}) & sequence indices (x{sequenceIndices.Count}) were unequal.");
            }

            IdentifyEarliestStartAndLatestEnd(startPositions, endPositions, out int earliestStart, out int latestEnd);

            List<bool[]> reads = CollectPaddedReads(alignment, sequenceIndices, startPositions, endPositions);

            return new CharacterBlock(earliestStart, sequenceIndices, reads);
        }

        public List<bool[]> CollectPaddedReads(in MaskedAlignment alignment, in List<int> sequenceIndices, in List<int> startPositions, in List<int> endPositions)
        {
            IdentifyEarliestStartAndLatestEnd(startPositions, endPositions, out int earliestStart, out int latestEnd);

            int blockWidth = 1 + latestEnd - earliestStart;

            List<bool[]> result = new List<bool[]>();

            for (int i = 0; i < sequenceIndices.Count; i++)
            {
                int seqIndex = sequenceIndices[i];
                int startPos = startPositions[i];
                int endPos = startPositions[i];
                PaddingInstructions padding = CalculatePadding(startPos, endPos, earliestStart, latestEnd);
                bool[] paddedRead = GetPaddedRead(alignment, seqIndex, startPos, endPos, padding);
                result.Add(paddedRead);
            }

            return result;
        }

        public void IdentifyEarliestStartAndLatestEnd(in List<int> startPositions, in List<int> endPositions, out int earliestStart, out int latestEnd)
        {
            earliestStart = startPositions[0];
            latestEnd = endPositions[0];

            for (int i = 0; i < startPositions.Count; i++)
            {
                int start = startPositions[i];
                earliestStart = Math.Min(earliestStart, start);

                int end = endPositions[i];
                latestEnd = Math.Max(latestEnd, end);
            }
        }

        public PaddingInstructions CalculatePadding(int jStart, int jEnd, int earliestStart, int latestEnd)
        {
            int startPadding = jStart - earliestStart;
            int endPadding = latestEnd - jEnd;
            int blockWidth = 1 + latestEnd - earliestStart;

            return new PaddingInstructions(startPadding, endPadding, blockWidth);
        }

        public bool[] GetPaddedRead(MaskedAlignment alignment, int i, int jStart, int jEnd, PaddingInstructions padding)
        {
            bool[] result = new bool[padding.WidthGoal];

            int p = 0;
            for(int r = 0; r<padding.Start; r++)
            {
                result[p++] = false;
            }
            for(int j=jStart; j<=jEnd; j++)
            {
                result[p++] = alignment.Mask[i, j];
            }
            for (int r = 0; r < padding.End; r++)
            {
                result[p++] = false;
            }

            return result;
        }

        public void FindBlock(MaskedAlignment alignment, ref bool[] sequences, ref List<int> startPositions, ref List<int> endPositions)
        {
            PickStartingPosition(alignment, sequences, out int iStart, out int jStart);

            Console.WriteLine("PickStartingPosition() exited");

            for (int i=0; i<alignment.Height; i++)
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
                jEndInclusive += 1;
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
