using LibBioInfo.IAlignmentModifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness.Tools;
using TestsHarness;
using LibBioInfo;

namespace TestsUnitSuite.LibBioInfo.IAlignmentModifiers
{
    [TestClass]
    public class PercentileGapShifterTests
    {
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;
        AlignmentConservation AlignmentConservation = Harness.AlignmentConservation;

        IAlignmentModifier PercentileGapShifter = new PercentileGapShifter(0.05);

        [TestMethod]
        public void AlignmentIsDifferentAfterShift()
        {
            Alignment original = ExampleAlignments.GetExampleA();
            Alignment copy = original.GetCopy();
            PercentileGapShifter.ModifyAlignment(copy);

            bool alignmentsMatch = AlignmentEquality.AlignmentsMatch(original, copy);
            Assert.IsFalse(alignmentsMatch);

            AlignmentConservation.AssertAlignmentsAreConserved(copy, original);
        }

        [TestMethod]
        public void AlignmentStateIsDifferentAfterShift()
        {
            Alignment original = ExampleAlignments.GetExampleA();
            Alignment copy = original.GetCopy();
            PercentileGapShifter.ModifyAlignment(copy);

            bool alignmentsStatesMatch = AlignmentEquality.AlignmentsMatch(original, copy);
            Assert.IsFalse(alignmentsStatesMatch);
        }
    }
}
