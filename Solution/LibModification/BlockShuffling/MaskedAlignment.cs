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

        public MaskedAlignment(Alignment alignment, bool[,] mask, bool residuesAsOnes = true)
        {
            Alignment = alignment;
            Mask = mask;
            ResidueMarker = residuesAsOnes;
            GapMarker = !ResidueMarker;
        }
    }
}
