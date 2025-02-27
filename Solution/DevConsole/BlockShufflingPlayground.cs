using LibBioInfo;
using LibFileIO;
using LibModification.BlockShuffling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConsole
{
    internal class BlockShufflingPlayground
    {

        public void Main()
        {
            FileHelper helper = new FileHelper();
            Alignment alignment = helper.ReadAlignmentFrom("clustalformat_BB11001.aln");


            AlignmentMaskMaker maker = new AlignmentMaskMaker();

            Console.WriteLine("Original alignment mask:");
            MaskedAlignment maskedAli = maker.GetMaskedAlignment(alignment);
            PrintMask(maskedAli.Mask);
            Console.WriteLine();

            Console.WriteLine("Extracting a block randomly:");
            BlockFinder finder = new BlockFinder();
            bool[] sequences = { true, true, false, false };
            CharacterBlock block = finder.FindBlock(maskedAli, ref sequences);
            PrintBlock(block);
            Console.WriteLine();

            Console.WriteLine("Subtracting the extracted block:");
            maskedAli.SubtractBlock(block, block.OriginalPosition);
            PrintMask(maskedAli.Mask);
            Console.WriteLine();

            Console.WriteLine("Pasting the extracted block in a new position:");

        }



        public void PrintBlock(CharacterBlock block)
        {

            Console.Write("sequences = ");
            foreach(int i in block.SequenceIndices)
            {
                Console.Write(i + ", ");
            }
            Console.WriteLine();

            Console.WriteLine($"originally at j={block.OriginalPosition}");
            PrintMask(block.Mask);
        }


        public void PrintMask(bool[,] mask)
        {
            int m = mask.GetLength(0);
            for (int i = 0; i < m; i++)
            {
                PrintMaskRow(mask, i);
            }
        }

        public void PrintMaskRow(bool[,] mask, int i)
        {
            int n = mask.GetLength(1);

            for(int j=0; j<n; j++)
            {
                char x = mask[i, j] ? 'X' : '-';
                Console.Write(x);
            }
            Console.WriteLine();
        }
    }
}
