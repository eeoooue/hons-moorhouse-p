using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.BlockShuffling
{
    internal class AlignmentMaskMaker
    {
        public MaskedAlignment GetMaskedAlignment(Alignment alignment, bool residuesAsOnes = true)
        {
            bool[,] mask = ExtractMask(alignment, residuesAsOnes);
            return new MaskedAlignment(alignment, mask, residuesAsOnes);
        }

        public bool[,] ExtractMask(Alignment alignment, bool residuesAsOnes = true)
        {
            // TODO: consider inserting empty columns first

            bool residueMarker = residuesAsOnes;
            bool gapMarker = !residuesAsOnes;

            int m = alignment.Height;
            int n = alignment.Width;

            bool[,] mask = new bool[m, n];

            for(int i=0; i<m; i++)
            {
                for(int j=0; j<n; j++)
                {
                    char x = alignment.CharacterMatrix[i, j];
                    bool isGap = Bioinformatics.IsGap(x);
                    mask[i, j] = isGap ? gapMarker : residueMarker;
                }
            }

            return mask;
        }
    }
}
