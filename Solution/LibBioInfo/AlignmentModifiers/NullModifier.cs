using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.AlignmentModifiers
{
    public class NullModifier : AlignmentModifier, IAlignmentModifier
    {
        protected override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            return alignment.CharacterMatrix;
        }
    }
}
