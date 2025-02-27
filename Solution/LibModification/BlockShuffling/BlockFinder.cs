using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.BlockShuffling
{
    internal struct PaddingInstructions
    {
        public int Start = 0;
        public int End = 0;

        public PaddingInstructions(int start, int end)
        {
            Start = start;
            End = end;
        }
    }






    internal class BlockFinder
    {
        // aims to find a block of ones based on logic described in SAGA

        public CharacterBlock FindBlock(MaskedAlignment alignment, ref bool[] sequences)
        {
            List<int> startPositions = new List<int>();
            List<int> endPositions = new List<int>();
            FindBlock(alignment, ref sequences, ref startPositions, ref endPositions);
            return CreateBlock(alignment, sequences, startPositions, endPositions);
        }

        public CharacterBlock CreateBlock(in MaskedAlignment alignment, in bool[] sequences, in List<int> startPositions, in List<int> endPositions)
        {
            int n = startPositions.Count;
            if (n != endPositions.Count)
            {
                throw new Exception($"Number of start positions (x{startPositions.Count}) & end positions (x{endPositions.Count}) were unequal.");
            }

            int earliestStart = startPositions[0];
            int latestEnd = endPositions[0];

            for(int i=0; i<n; i++)
            {
                int start = startPositions[i];
                earliestStart = Math.Min(earliestStart, start);

                int end = endPositions[i];
                latestEnd = Math.Max(latestEnd, end);
            }

            int blockWidth = 1 + latestEnd - earliestStart;





            throw new NotImplementedException();
        }

        public PaddingInstructions CalculatePadding(int jStart, int jEnd, int earliestStart, int latestEnd)
        {
            int startPadding = jStart - earliestStart;
            int endPadding = latestEnd - jEnd;

            return new PaddingInstructions(startPadding, endPadding);
        }

        public string GetPaddedRead(MaskedAlignment alignment, int i, int jStart, int jEnd, PaddingInstructions padding)
        {
            StringBuilder sb = new StringBuilder();

            for(int r = 0; r<padding.Start; r++)
            {
                sb.Append('-');
            }

            for(int j=jStart; j<=jEnd; j++)
            {
                sb.Append(alignment.Mask[i, j]);
            }

            for (int r = 0; r < padding.End; r++)
            {
                sb.Append('-');
            }

            return sb.ToString();
        }

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
