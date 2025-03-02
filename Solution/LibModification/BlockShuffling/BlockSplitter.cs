using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.BlockShuffling
{
    internal class BlockSplitter
    {
        public static CharacterBlock SplitBlock(CharacterBlock block)
        {
            int width = PickNewWidth(block);

            if (Randomizer.CoinFlip())
            {
                return ExtractLeftSide(block, width);
            }
            else
            {
                return ExtractRightSide(block, width);
            }
        }

        public static CharacterBlock ExtractLeftSide(CharacterBlock block, int width)
        {
            bool[,] mask = new bool[block.Height, width];

            for (int i = 0; i < block.Height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    mask[i, j] = block.Mask[i, j];
                }
            }

            return new CharacterBlock(block.OriginalPosition, block.SequenceIndices, mask);
        }

        public static CharacterBlock ExtractRightSide(CharacterBlock block, int width)
        {
            int charactersSkipped = block.Width - width;
            int newStartingPos = block.OriginalPosition + charactersSkipped;

            bool[,] mask = new bool[block.Height, width];

            for(int i=0; i<block.Height; i++)
            {
                int jRight = block.Width - 1;

                for(int j=0; j<width; j++)
                {
                    mask[i, j] = block.Mask[i, jRight];
                    jRight--;
                }
            }

            return new CharacterBlock(newStartingPos, block.SequenceIndices, mask);
        }

        public static int PickNewWidth(CharacterBlock block)
        {
            int n = block.Width;
            if (n <= 1)
            {
                return 1;
            }

            return Randomizer.Random.Next(1, n);
        }
    }
}
