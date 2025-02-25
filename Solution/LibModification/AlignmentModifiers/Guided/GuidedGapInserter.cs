﻿using LibBioInfo;
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
    public class GuidedGapInserter : AlignmentModifier, IAlignmentModifier
    {
        public BiosequencePayloadHelper PayloadHelper = new BiosequencePayloadHelper();
        public CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();
        private GapInsertion GapInsertion = new GapInsertion();

        public override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            int gapWidth = PickGapWidth(alignment);
            bool[] mapping = SimilarityGuide.GetSetOfSimilarSequencesAsMask(alignment);
            SuggestPairOfGapPositions(alignment, out int j1, out int j2);
            GapInsertion.InsertGaps(alignment, mapping, gapWidth, j1, j2);

            return CharMatrixHelper.RemoveEmptyColumns(alignment.CharacterMatrix);
        }

        public void SuggestPairOfGapPositions(Alignment alignment, out int j1, out int j2)
        {
            int limit = alignment.Width + 1;
            j1 = Randomizer.Random.Next(0, limit);
            j2 = j1;

            while (j1 == j2)
            {
                j2 = Randomizer.Random.Next(0, limit);
            }
        }

        public int PickGapWidth(Alignment alignment)
        {
            int n = alignment.Width;
            int gapWidth = Randomizer.Random.Next(1, n + 1);
            return gapWidth;
        }
    }
}
