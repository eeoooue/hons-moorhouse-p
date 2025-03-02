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

namespace LibModification.AlignmentModifiers
{
    public class HeuristicPairwiseModifier : AlignmentModifier
    {
        public override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            PickRandomPairOfSequences(alignment, out int i, out int j);
            return PairwiseAlignment.AlignPairOfSequences(alignment, i, j);
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
