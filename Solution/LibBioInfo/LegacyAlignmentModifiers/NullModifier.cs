using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.LegacyAlignmentModifiers
{
    public class NullModifier : ILegacyAlignmentModifier
    {
        public void ModifyAlignment(Alignment alignment)
        {
            return;
        }
    }
}
