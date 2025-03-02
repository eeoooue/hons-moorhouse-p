using LibBioInfo;
using LibBioInfo.PairwiseAligners;
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
    public class GuidedMultiplePairwiseModifier : AlignmentModifier
    {
        public override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            return AlignSetOfSimilarSequencesAtOnce(alignment);
        }

        public char[,] AlignSetOfSimilarSequencesAtOnce(Alignment alignment)
        {
            List<BioSequence> selection = SimilarityGuide.GetSetOfSimilarSequences();

            int previous = alignment.GetIndexOfSequence(selection[0]);
            for (int i = 1; i < selection.Count; i++)
            {
                int current = alignment.GetIndexOfSequence(selection[i]);
                alignment.CharacterMatrix = PairwiseAlignment.AlignPairOfSequences(alignment, previous, current);
                previous = current;
            }

            return alignment.CharacterMatrix;
        }
    }
}
