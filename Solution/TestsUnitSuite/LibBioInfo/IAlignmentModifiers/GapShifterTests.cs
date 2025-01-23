using LibBioInfo;
using LibBioInfo.ScoringMatrices;

using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibFileIO;

using TestsHarness;
using TestsHarness.Tools;
using LibModification;
using LibModification.AlignmentModifiers;

namespace TestsUnitSuite.LibBioInfo.IAlignmentModifiers
{
    [TestClass]
    public class GapShifterTests
    {
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;
        AlignmentConservation AlignmentConservation = Harness.AlignmentConservation;

        GapShifter GapShifter = new GapShifter();

        [TestMethod]
        public void AlignmentIsDifferentAfterShift()
        {
            Alignment original = ExampleAlignments.GetExampleA();
            Alignment copy = original.GetCopy();
            GapShifter.ModifyAlignment(copy);

            bool alignmentsMatch = AlignmentEquality.AlignmentsMatch(original, copy);
            Assert.IsFalse(alignmentsMatch);

            AlignmentConservation.AssertAlignmentsAreConserved(copy, original);
        }

        [TestMethod]
        public void AlignmentStateIsDifferentAfterShift()
        {
            Alignment original = ExampleAlignments.GetExampleA();
            Alignment copy = original.GetCopy();
            GapShifter.ModifyAlignment(copy);

            bool alignmentsStatesMatch = AlignmentEquality.AlignmentsMatch(original, copy);
            Assert.IsFalse(alignmentsStatesMatch);
        }
    }
}
