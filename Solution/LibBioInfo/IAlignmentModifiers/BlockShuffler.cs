using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.IAlignmentModifiers
{
    public class BlockShuffler : IAlignmentModifier
    {
        public void ModifyAlignment(Alignment alignment)
        {
            int i = Randomizer.Random.Next(alignment.Height);
            PerformBlockShuffleInRow(alignment, i);
            alignment.CheckResolveEmptyColumns();
        }

        public void PerformBlockShuffleInRow(Alignment alignment, int i)
        {
            int startPos = 0;
            int endPos = 0;
            PickBlockRangeInRow(alignment, i, out startPos, out endPos);
            List<int> options = GetNewValidStartPositions(alignment, i, startPos, endPos);

            if (options.Count > 0)
            {
                int length = 1 + endPos - startPos;
                int j1 = PickRandomPositionFromOptions(options);
                int j2 = j1 + length - 1;
                ClearResiduesAt(alignment, i, startPos, endPos);
                PlaceBlockAt(alignment, i, j1, j2);
            }
        }

        public void PickBlockRangeInRow(Alignment alignment, int i, out int startPos, out int endPos)
        {
            List<int> positions = alignment.GetResiduePositionsInRow(alignment, i);
            startPos = PickRandomPositionFromOptions(positions);
            endPos = DetermineBlockEnd(alignment, i, startPos);
        }

        public List<int> GetNewValidStartPositions(Alignment alignment, int i, int startPos, int endPos)
        {
            List<int> result = new List<int>();
            CollectValidBackShifts(alignment, i, startPos, endPos, result);
            CollectValidForwardShifts(alignment, i, startPos, endPos, result);
            return result;
        }

        public void CollectValidForwardShifts(Alignment alignment, int i, int startPos, int endPos, List<int> options)
        {
            int offset = 0;
            for (int j = endPos + 1; j < alignment.Width; j++)
            {
                offset++;
                if (alignment.State[i, j])
                {
                    options.Add(startPos + offset);
                }
                else
                {
                    return;
                }
            }
        }

        public void CollectValidBackShifts(Alignment alignment, int i, int startPos, int endPos, List<int> options)
        {
            int offset = 0;
            for (int j=startPos-1; j>=0; j--)
            {
                offset++;
                if (alignment.State[i, j])
                {
                    options.Add(startPos - offset);
                }
                else
                {
                    return;
                }
            }
        }

        public void ClearResiduesAt(Alignment alignment, int i, int startPos, int endPos)
        {
            for(int j=startPos; j<=endPos; j++)
            {
                alignment.State[i, j] = true;
            }
        }

        public void PlaceBlockAt(Alignment alignment, int i, int startPos, int endPos)
        {
            for (int j = startPos; j <= endPos; j++)
            {
                alignment.State[i, j] = false;
            }
        }

        

        public int DetermineBlockEnd(Alignment alignment, int i, int startPos)
        {
            for(int j=startPos; j<alignment.Width; j++)
            {
                if (alignment.State[i, j])
                {
                    return j - 1;
                }
            }

            return alignment.Width - 1;
        }

        public int PickRandomPositionFromOptions(List<int> options)
        {
            int i = Randomizer.Random.Next(options.Count);
            return options[i];
        }
    }
}
