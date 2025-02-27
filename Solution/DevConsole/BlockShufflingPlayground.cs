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

            MaskedAlignment maskedAli = maker.GetMaskedAlignment(alignment);
            PrintMask(maskedAli.Mask);

            BlockFinder finder = new BlockFinder();

            bool[] sequences = { true, true, false, false };

            Console.WriteLine();
            CharacterBlock block = finder.FindBlock(maskedAli, ref sequences);
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
