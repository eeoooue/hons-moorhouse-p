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
    public class GuidedResidueShifter : AlignmentModifier
    {
        private CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();

        public override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            List<BioSequence> sequences = SimilarityGuide.GetSetOfSimilarSequences();
            HashSet<string> identifiers = new HashSet<string>();
            foreach(BioSequence sequence in sequences)
            {
                identifiers.Add(sequence.Identifier);
            }

            int j = Randomizer.Random.Next(alignment.Width);
            ShiftDirection direction = Randomizer.CoinFlip() ? ShiftDirection.Leftwise : ShiftDirection.Rightwise;

            // TODO: consider case when width changes ? i.e. does j need updating

            for (int i=0; i < alignment.Height; i++)
            {
                BioSequence sequence = alignment.Sequences[i];
                if (identifiers.Contains(sequence.Identifier))
                {
                    char x = alignment.CharacterMatrix[i, j];
                    if (!Bioinformatics.IsGap(x))
                    {
                        ResidueShift.ShiftResidue(alignment, i, j, direction);
                    }
                }
            }

            return CharMatrixHelper.RemoveEmptyColumns(alignment.CharacterMatrix);
        }
    }
}
