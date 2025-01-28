using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.AlignmentModifiers
{
    public class NullModifier : AlignmentModifier, IAlignmentModifier
    {
        public override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            return alignment.CharacterMatrix;
        }
    }
}
