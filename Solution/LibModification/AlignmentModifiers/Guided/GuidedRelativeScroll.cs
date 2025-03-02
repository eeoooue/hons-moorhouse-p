using LibBioInfo;
using LibModification.Helpers;
using LibModification.Mechanisms;
using LibSimilarity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.AlignmentModifiers.Guided
{
    public class GuidedRelativeScroll : AlignmentModifier
    {
        private CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();

        public int KMerSize = 4;

        public override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            bool[] mask = SimilarityGuide.GetSetOfSimilarSequencesAsMask(alignment);
            int i = PickRandomIndexOfValue(mask, true);
            int j = PickRandomIndexOfValue(mask, false);

            int offset = PickRelativeOffset(alignment, i, j);

            if (offset == 0)
            {
                return alignment.CharacterMatrix;
            }

            char[,] matrix = GetMatrixAfterOffset(alignment, mask, offset);

            return CharMatrixHelper.RemoveEmptyColumns(matrix);
        }


        public char[,] GetMatrixAfterOffset(Alignment alignment, bool[] mask, int offset)
        {
            int width = Math.Abs(offset);
            int n = alignment.Width;

            if (offset < 0)
            {
                GapInsertion.InsertGaps(alignment, mask, width, n, 0);
            }
            else if (offset > 0)
            {
                GapInsertion.InsertGaps(alignment, mask, width, 0, n);
            }

            return alignment.CharacterMatrix;
        }

        public int PickRelativeOffset(Alignment alignment, int i1, int i2)
        {
            List<int> candidates = CollectKmers(alignment, i1);
            List<int> possiblePairs = CollectKmers(alignment, i2);

            if (candidates.Count == 0 || possiblePairs.Count == 0)
            {
                return 0;
            }

            int candidate = PickIntFromList(candidates);
            List<int> matches = FilterToMatchesOnly(alignment, i1, candidate, i2, possiblePairs);
            if (matches.Count == 0)
            {
                return 0;
            }

            int match = PickIntFromList(matches);

            return match - candidate; // distance match is ahead of candidate (may be negative)
        }

        public List<int> FilterToMatchesOnly(Alignment alignment, int i1, int j1, int i2, List<int> possiblePairs)
        {
            List<int> result = new List<int>();
            foreach(int j2 in possiblePairs)
            {
                bool matchFound = KMersMatch(alignment.CharacterMatrix, i1, j1, i2, j2);
                if (matchFound)
                {
                    result.Add(j2);
                }
            }

            return result;
        }

        public int PickIntFromList(List<int> options)
        {
            int i = Randomizer.Random.Next(options.Count);
            return options[i];
        }

        public List<int> CollectKmers(Alignment alignment, int i)
        {
            List<int> result = new List<int>();
            for(int j=0; j<alignment.Width; j++)
            {
                if (IsKMer(alignment.CharacterMatrix, i, j))
                {
                    result.Add(j);
                }
            }

            return result;
        }

        public bool KMersMatch(char[,] matrix, int i1, int j1, int i2, int j2)
        {
            int n = matrix.GetLength(1);

            if (j1 + KMerSize >= n)
            {
                return false;
            }

            for (int r = 0; r < KMerSize; r++)
            {
                char x = matrix[i1, j1 + r];
                char y = matrix[i2, j2 + r];
                if (x != y)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsKMer(char[,] matrix, int i, int j)
        {
            int n = matrix.GetLength(1);

            if (j+KMerSize >= n)
            {
                return false;
            }

            for(int r=0; r<KMerSize; r++)
            {
                char x = matrix[i, j + r];
                if (Bioinformatics.IsGap(x))
                {
                    return false;
                }
            }

            return true;
        }

        public int PickRandomIndexOfValue(bool[] mask, bool flag)
        {
            List<int> candidates = new List<int>();
            for(int i=0; i<mask.Length; i++)
            {
                if (mask[i] == flag)
                {
                    candidates.Add(i);
                }
            }

            int index = Randomizer.Random.Next(candidates.Count);
            return candidates[index];
        }
    }
}
