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
    public class GuidedSwap : AlignmentModifier
    {
        private CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();

        public override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            HashSet<string> identifiers = GetHashsetOfIdentifiers(alignment);

            GetRandomParams(alignment, out int j, out int k, out SwapDirection direction);

            for (int i = 0; i < alignment.Height; i++)
            {
                BioSequence sequence = alignment.Sequences[i];
                if (identifiers.Contains(sequence.Identifier))
                {
                    char x = alignment.CharacterMatrix[i, j];
                    if (!Bioinformatics.IsGap(x))
                    {
                        SwapMSASA.Swap(alignment, i, j, k, direction);
                    }
                }
            }

            return CharMatrixHelper.RemoveEmptyColumns(alignment.CharacterMatrix);
        }

        public HashSet<string> GetHashsetOfIdentifiers(Alignment alignment)
        {
            List<BioSequence> sequences = SimilarityGuide.GetSetOfSimilarSequences(alignment);
            HashSet<string> result = new HashSet<string>();
            foreach (BioSequence sequence in sequences)
            {
                result.Add(sequence.Identifier);
            }

            return result;
        }

        public void GetRandomParams(Alignment alignment, out int j, out int k, out SwapDirection direction)
        {
            int n = alignment.Width;

            j = n;
            k = n;
            while (j + k >= n)
            {
                j = Randomizer.Random.Next(n);
                k = Randomizer.Random.Next(1, n / 2);
            }

            direction = Randomizer.CoinFlip() ? SwapDirection.Left : SwapDirection.Right;
        }
    }
}
