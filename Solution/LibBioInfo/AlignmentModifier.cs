using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo
{
    public abstract class AlignmentModifier : IAlignmentModifier
    {
        public void ModifyAlignment(Alignment alignment)
        {
            char[,] modifiedMat = GetModifiedAlignmentState(alignment);
            alignment.CharacterMatrix = modifiedMat;
        }

        protected abstract char[,] GetModifiedAlignmentState(Alignment alignment);
    }
}
