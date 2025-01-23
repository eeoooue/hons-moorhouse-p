using LibBioInfo;
using LibBioInfo.IAlignmentModifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsUnitSuite.LibBioInfo.IAlignmentModifiers
{
    [TestClass]
    public class MultiOperatorModifierTests
    {
        [TestMethod]
        public void CanSelectRandomOperator()
        {
            List<ILegacyAlignmentModifier> modifiers = new List<ILegacyAlignmentModifier>()
            {
                new GapShifter(),
                new GapInserter(),
            };

            MultiOperatorModifier multi = new MultiOperatorModifier(modifiers);
            ILegacyAlignmentModifier selection = multi.PickRandomModifier();
        }
    }
}
