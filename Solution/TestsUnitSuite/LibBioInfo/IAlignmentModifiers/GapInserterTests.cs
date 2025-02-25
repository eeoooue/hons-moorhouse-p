using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness.Tools;
using TestsHarness;
using LibModification;
using LibModification.AlignmentModifiers;

namespace TestsUnitSuite.LibBioInfo.IAlignmentModifiers
{
    [TestClass]
    public class GapInserterTests
    {
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;
        AlignmentConservation AlignmentConservation = Harness.AlignmentConservation;

        GapInserter GapInserter = new GapInserter();

        [TestMethod]
        public void AlignmentIsDifferentAfterInsertion()
        {
            Alignment original = ExampleAlignments.GetExampleA();
            Alignment copy = original.GetCopy();
            GapInserter.ModifyAlignment(copy);

            bool alignmentsMatch = AlignmentEquality.AlignmentsMatch(original, copy);
            Assert.IsFalse(alignmentsMatch);

            AlignmentConservation.AssertAlignmentsAreConserved(copy, original);
        }

        [TestMethod]
        public void AlignmentStateIsDifferentAfterShift()
        {
            Alignment original = ExampleAlignments.GetExampleA();
            Alignment copy = original.GetCopy();
            GapInserter.ModifyAlignment(copy);

            bool alignmentsStatesMatch = AlignmentEquality.AlignmentsMatch(original, copy);
            Assert.IsFalse(alignmentsStatesMatch);
        }
    }
}
