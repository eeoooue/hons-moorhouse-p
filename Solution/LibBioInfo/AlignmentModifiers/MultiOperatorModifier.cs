using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.AlignmentModifiers
{
    public class MultiOperatorModifier : ILegacyAlignmentModifier
    {
        List<ILegacyAlignmentModifier> Modifiers;

        public MultiOperatorModifier(List<ILegacyAlignmentModifier> modifiers)
        {
            Modifiers = modifiers;
        }

        public void ModifyAlignment(Alignment alignment)
        {
            ILegacyAlignmentModifier modifier = PickRandomModifier();
            modifier.ModifyAlignment(alignment);
        }

        public ILegacyAlignmentModifier PickRandomModifier()
        {
            int n = Modifiers.Count;
            int i = Randomizer.Random.Next(0, n);
            return Modifiers[i];
        }
    }
}
