using LibBioInfo;
using LibModification.BlockShuffling;
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
    public class GapShuffler : AlignmentModifier
    {
        public override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            alignment.CharacterMatrix = ColumnInsertion.AddEmptyColumnsAsPadding(alignment.CharacterMatrix, 1, 1);

            MaskedAlignment maskedAli = new MaskedAlignment(alignment, false);

            BlockFinder finder = new BlockFinder();

            bool[] mask = SimilarityGuide.GetSetOfSimilarSequencesAsMask(alignment);
            //if (Randomizer.CoinFlip())
            //{
            //    TryInvertMask(mask);
            //}

            CharacterBlock block = finder.FindBlock(maskedAli, ref mask);
            //if (Randomizer.CoinFlip())
            //{
            //    block = BlockSplitter.SplitBlock(block);
            //}

            maskedAli.SubtractBlock(block, block.OriginalPosition);

            List<int> possibleNewPositions = maskedAli.GetPossibleNewPositionsForBlock(block);

            if (possibleNewPositions.Count > 0)
            {
                int newPosition = Randomizer.PickIntFromList(possibleNewPositions);
                maskedAli.PlaceBlock(block, newPosition);
            }
            else
            {
                maskedAli.PlaceBlock(block, block.OriginalPosition);
            }

            char[,] modified = maskedAli.ExtractAlignment();

            return CharMatrixHelper.RemoveEmptyColumns(modified);
        }

        private void TryInvertMask(bool[] mask)
        {
            if (mask.Contains(false))
            {
                for(int i=0; i<mask.Length; i++)
                {
                    mask[i] = !mask[i];
                }
            }
        }
    }
}
