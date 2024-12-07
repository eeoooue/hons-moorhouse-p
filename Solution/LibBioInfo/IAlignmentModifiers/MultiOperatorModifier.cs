using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.IAlignmentModifiers
{
    public class MultiOperatorModifier : IAlignmentModifier
    {
        List<IAlignmentModifier> Modifiers;

        public MultiOperatorModifier(List<IAlignmentModifier> modifiers)
        {
            Modifiers = modifiers;
        }

        public void ModifyAlignment(Alignment alignment)
        {
            IAlignmentModifier modifier = PickRandomModifier();
            modifier.ModifyAlignment(alignment);
        }

        public IAlignmentModifier PickRandomModifier()
        {
            int n = Modifiers.Count;
            int i = Randomizer.Random.Next(0, n);
            return Modifiers[i];
        }
    }
}
