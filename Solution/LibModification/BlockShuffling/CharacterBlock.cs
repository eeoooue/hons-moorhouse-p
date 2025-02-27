using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.BlockShuffling
{
    internal class CharacterBlock
    {
        public List<int> SequenceIndices;

        public bool[,] Mask;

        public int Width;

        public CharacterBlock(List<int> sequences, bool[,] mask)
        {
            SequenceIndices = sequences;
            Mask = mask;
            Width = Mask.GetLength(1);
        }
    }
}
