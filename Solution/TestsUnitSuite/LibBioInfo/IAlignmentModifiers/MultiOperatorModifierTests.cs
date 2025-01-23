using LibBioInfo;
using LibBioInfo.AlignmentModifiers;
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
            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            {
                new GapShifter(),
                new GapInserter(),
            };

            MultiOperatorModifier multi = new MultiOperatorModifier(modifiers);
            IAlignmentModifier selection = multi.PickRandomModifier();
        }
    }
}
