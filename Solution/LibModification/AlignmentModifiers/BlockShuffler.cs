using LibBioInfo;
using LibModification.BlockShuffling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.AlignmentModifiers
{
    public class BlockShuffler : AlignmentModifier
    {
        public override char[,] GetModifiedAlignmentState(Alignment alignment)
        {

            AlignmentMaskMaker maker = new AlignmentMaskMaker();

            MaskedAlignment maskedAli = maker.GetMaskedAlignment(alignment);

            Console.WriteLine("Extracting a block randomly:");
            BlockFinder finder = new BlockFinder();
            bool[] sequences = { true, true, false, false };
            CharacterBlock block = finder.FindBlock(maskedAli, ref sequences);

            maskedAli.SubtractBlock(block, block.OriginalPosition);

            List<int> possibleNewPositions = maskedAli.GetPossibleNewPositionsForBlock(block);

            if (possibleNewPositions.Count == 0)
            {
                return alignment.CharacterMatrix;
            }

            int newPosition = Randomizer.PickIntFromList(possibleNewPositions);
            maskedAli.PlaceBlock(block, newPosition);


            // return maskedAli.get

            throw new NotImplementedException();
        }
    }
}
