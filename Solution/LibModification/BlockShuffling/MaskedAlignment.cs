using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.BlockShuffling
{
    public class MaskedAlignment
    {
        public Alignment Alignment;
        public bool[,] Mask;
        public bool ResidueMarker;
        public bool GapMarker;

        public int Height { get { return Alignment.Height; } }
        public int Width { get { return Alignment.Width; } }

        public MaskedAlignment(Alignment alignment, bool[,] mask, bool residuesAsOnes = true)
        {
            Alignment = alignment;
            Mask = mask;
            ResidueMarker = residuesAsOnes;
            GapMarker = !ResidueMarker;
        }

        public MaskedAlignment(Alignment alignment, bool residuesAsOnes = true)
        {
            Alignment = alignment;
            Mask = CreateMask(alignment, residuesAsOnes);
            ResidueMarker = residuesAsOnes;
            GapMarker = !ResidueMarker;
        }

        public bool[,] CreateMask(Alignment alignment, bool residuesAsOnes = true)
        {
            // TODO: consider inserting empty columns first

            bool residueMarker = residuesAsOnes;
            bool gapMarker = !residuesAsOnes;

            int m = alignment.Height;
            int n = alignment.Width;

            bool[,] mask = new bool[m, n];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    char x = alignment.CharacterMatrix[i, j];
                    bool isGap = Bioinformatics.IsGap(x);
                    mask[i, j] = isGap ? gapMarker : residueMarker;
                }
            }

            return mask;
        }

        public void SubtractBlock(CharacterBlock block, int jOffset)
        {

            int n = block.Height;

            for(int i=0; i<n; i++)
            {
                int sequenceIndex = block.SequenceIndices[i];
                SubtractBlockRow(block, jOffset, i, sequenceIndex);
            }
        }

        private void SubtractBlockRow(CharacterBlock block, int jOffset, int blockMaskIndex, int sequenceIndex)
        {
            for(int j=0; j<block.Width; j++)
            {
                if (block.Mask[blockMaskIndex, j])
                {
                    Mask[sequenceIndex, j + jOffset] = false;
                }
            }
        }

        public bool CanPlaceBlock(CharacterBlock block, int jOffset)
        {
            int n = block.Height;
            for (int i = 0; i < n; i++)
            {
                int sequenceIndex = block.SequenceIndices[i];
                bool possible = CanPlaceBlockRow(block, jOffset, i, sequenceIndex);
                if (!possible)
                {
                    return false;
                }
            }

            return true;
        }

        public bool CanPlaceBlockRow(CharacterBlock block, int jOffset, int blockMaskIndex, int sequenceIndex)
        {
            if (block.Width + jOffset > Width)
            {
                return false;
            }

            if (jOffset < 0)
            {
                return false;
            }

            for (int j = 0; j < block.Width; j++)
            {
                if (block.Mask[blockMaskIndex, j])
                {
                    bool valueAtPos = Mask[sequenceIndex, j + jOffset];
                    if (valueAtPos)
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        public void PlaceBlock(CharacterBlock block, int jOffset)
        {
            int n = block.Height;
            for (int i = 0; i < n; i++)
            {
                int sequenceIndex = block.SequenceIndices[i];
                PlaceBlockRow(block, jOffset, i, sequenceIndex);
            }
        }

        public void PlaceBlockRow(CharacterBlock block, int jOffset, int blockMaskIndex, int sequenceIndex)
        {
            for (int j = 0; j < block.Width; j++)
            {
                if (block.Mask[blockMaskIndex, j])
                {
                    Mask[sequenceIndex, j + jOffset] = block.Mask[blockMaskIndex, j];
                }
            }
        }

        public List<int> GetPossibleNewPositionsForBlock(CharacterBlock block)
        {
            List<int> result = new List<int>();

            int jPos = block.OriginalPosition;
            while (true) // checking left
            {
                jPos -= 1;
                if (CanPlaceBlock(block, jPos))
                {
                    result.Add(jPos);
                }
                else
                {
                    break;
                }
            }

            jPos = block.OriginalPosition;
            while (true) // checking right
            {
                jPos += 1;
                if (CanPlaceBlock(block, jPos))
                {
                    result.Add(jPos);
                }
                else
                {
                    break;
                }
            }

            return result;
        }


    }
}
