using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.LegacyAlignmentModifiers
{
    public class NullModifier : AlignmentModifier, ILegacyAlignmentModifier
    {
        protected override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            return alignment.CharacterMatrix;
        }
    }
}
