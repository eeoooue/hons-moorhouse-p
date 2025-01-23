using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness.Tools;
using TestsHarness;
using LibBioInfo.AlignmentModifiers;

namespace TestsUnitSuite.LibBioInfo.IAlignmentModifiers
{
    [TestClass]
    public class MultiRowStochasticGapShifterTests
    {
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;
        AlignmentConservation AlignmentConservation = Harness.AlignmentConservation;

        IAlignmentModifier MultiRowStochasticGapShifter = new MultiRowStochasticGapShifter();

        [TestMethod]
        public void AlignmentIsDifferentAfterShift()
        {
            Alignment original = ExampleAlignments.GetExampleA();
            Alignment copy = original.GetCopy();
            MultiRowStochasticGapShifter.ModifyAlignment(copy);

            bool alignmentsMatch = AlignmentEquality.AlignmentsMatch(original, copy);
            Assert.IsFalse(alignmentsMatch);

            AlignmentConservation.AssertAlignmentsAreConserved(copy, original);
        }

        [DataTestMethod]
        [DataRow(525)]
        public void AlignmentStateIsDifferentAfterShift(int seed)
        {
            Randomizer.SetSeed(seed);
            Alignment original = ExampleAlignments.GetExampleA();
            Alignment copy = original.GetCopy();
            MultiRowStochasticGapShifter.ModifyAlignment(copy);

            bool alignmentsStatesMatch = AlignmentEquality.AlignmentsMatch(original, copy);
            Assert.IsFalse(alignmentsStatesMatch);
        }
    }
}
