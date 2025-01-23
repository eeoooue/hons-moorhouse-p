using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.LegacyAlignmentModifiers
{
    public class NullModifier : ILegacyAlignmentModifier, IAlignmentModifier
    {
        public void ModifyAlignment(Alignment alignment)
        {
            char[,] modified = GetModifiedAlignmentState(alignment);
            alignment.CharacterMatrix = modified;
        }

        public char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            return alignment.CharacterMatrix;
        }
    }
}
