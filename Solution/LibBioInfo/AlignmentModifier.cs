using LibBioInfo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo
{
    public abstract class AlignmentModifier : IAlignmentModifier, ILegacyAlignmentModifier
    {
        CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();

        public void ModifyAlignment(Alignment alignment)
        {
            ModifyAlignmentMatrix(ref alignment.CharacterMatrix);
            alignment.CharacterMatrix = CharMatrixHelper.RemoveEmptyColumns(in alignment.CharacterMatrix);
        }

        public abstract void ModifyAlignmentMatrix(ref char[,] matrix);


        public char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            throw new NotImplementedException();
        }
    }
}
