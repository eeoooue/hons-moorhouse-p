using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.BlockShuffling
{
    public class CharacterBlock
    {
        public List<int> SequenceIndices;

        public bool[,] Mask;

        public int Height;

        public int Width;

        public int OriginalPosition;

        public CharacterBlock(int originalPosition, List<int> sequences, bool[,] mask)
        {
            OriginalPosition = originalPosition;
            SequenceIndices = sequences;
            Mask = mask;
            Width = Mask.GetLength(1);
            Height = sequences.Count;
        }

        public CharacterBlock(int originalPosition, List<int> sequences, List<bool[]> reads)
        {
            OriginalPosition = originalPosition;
            SequenceIndices = sequences;

            Height = sequences.Count;
            Width = reads[0].Length;

            Mask = new bool[Height, Width];
            WriteReadsOntoMask(reads, Mask);
        }

        public void WriteReadsOntoMask(List<bool[]> reads, bool[,] mask)
        {
            int m = mask.GetLength(0);
            int n = mask.GetLength(1);

            for (int i=0; i<m; i++)
            {
                bool[] read = reads[i];
                for(int j=0; j<n; j++)
                {
                    mask[i, j] = read[j];
                }
            }
        }
    }
}
