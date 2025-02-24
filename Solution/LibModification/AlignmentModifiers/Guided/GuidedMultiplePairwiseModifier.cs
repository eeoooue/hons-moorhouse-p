using LibBioInfo;
using LibBioInfo.PairwiseAligners;
using LibModification.Helpers;
using LibSimilarity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.AlignmentModifiers.Guided
{
    public class GuidedMultiplePairwiseModifier : AlignmentModifier
    {
        HeuristicPairwiseModifier PairwiseModifier = new HeuristicPairwiseModifier();

        public char[,] AlignSetOfSimilarSequencesAtOnce(Alignment alignment)
        {
            List<BioSequence> selection = SimilarityGuide.GetSetOfSimilarSequences();

            int previous = alignment.GetIndexOfSequence(selection[0]);
            for (int i = 1; i < selection.Count; i++)
            {
                int current = alignment.GetIndexOfSequence(selection[i]);
                alignment.CharacterMatrix = PairwiseModifier.AlignPairOfSequences(alignment, previous, current);
                previous = current;
            }

            return alignment.CharacterMatrix;
        }

        public override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            return AlignSetOfSimilarSequencesAtOnce(alignment);
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
